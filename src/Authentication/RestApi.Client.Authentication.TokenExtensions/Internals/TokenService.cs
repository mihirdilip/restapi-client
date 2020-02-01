// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	internal interface ITokenService : IDisposable
	{
		Task<TokenResponse> ProcessRequestAsync(string providerName, ITokenRequest request, CancellationToken cancellationToken);
	}

	internal class TokenService : ITokenService
	{
		private readonly HttpClient _httpClient;
		private readonly IEnumerable<TokenProviderConfig> _tokenProviderConfigs;
		private readonly ConcurrentDictionary<string, TokenResponse> _cachedTokenResponses = new ConcurrentDictionary<string, TokenResponse>();
		private readonly ConcurrentDictionary<string, string> _cachedAccessTokensKeys = new ConcurrentDictionary<string, string>();
		private readonly ConcurrentDictionary<string, string> _cachedRefreshTokensKeys = new ConcurrentDictionary<string, string>();

		public TokenService(IHttpClientFactory httpClientFactory, IEnumerable<TokenProviderConfig> tokenProviderConfigs)
		{
			_httpClient = httpClientFactory.CreateClient(typeof(TokenService).FullName);
			_tokenProviderConfigs = tokenProviderConfigs;
		}

		public Task<TokenResponse> ProcessRequestAsync(string providerName, ITokenRequest request, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			if (string.IsNullOrWhiteSpace(providerName)) throw new ArgumentNullException(nameof(providerName));
			if (request == null) throw new ArgumentNullException(nameof(request));

			switch (request)
			{
				case PasswordCredentialsTokenRequest tokenRequest:
					return RequestPasswordCredentialsTokenAsync(providerName, tokenRequest, cancellationToken);

				case ClientCredentialsTokenRequest tokenRequest:
					return RequestClientCredentialsTokenAsync(providerName, tokenRequest, cancellationToken);

				case RefreshTokenRequest tokenRequest:
					return RequestRefreshTokenAsync(providerName, tokenRequest, cancellationToken);

				case RevokeTokenRequest tokenRequest:
					return RequestRevokeTokenAsync(providerName, tokenRequest, cancellationToken);

				default:
					throw new InvalidOperationException("Token request type not recognized.");
			}
		}

		private Task<TokenResponse> RequestPasswordCredentialsTokenAsync(string providerName, PasswordCredentialsTokenRequest request, CancellationToken cancellationToken)
		{
			return RequestTokenWithCachingAsync(providerName, request, cancellationToken);
		}

		private Task<TokenResponse> RequestClientCredentialsTokenAsync(string providerName, ClientCredentialsTokenRequest request, CancellationToken cancellationToken)
		{
			return RequestTokenWithCachingAsync(providerName, request, cancellationToken);
		}

		private async Task<TokenResponse> RequestRefreshTokenAsync(string providerName, RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var config = _tokenProviderConfigs.FirstOrDefault(o => o.ProviderName?.Equals(providerName, StringComparison.OrdinalIgnoreCase) ?? false);
			if (config == null) throw new InvalidOperationException($"No token provider is setup with provider name '{providerName}'.");

			_cachedRefreshTokensKeys.TryGetValue(request.RefreshToken, out var cacheKey);
			RemoveFromCacheByRefreshToken(request.RefreshToken);

			var tokenResponse = await PrivateTokenRequestAsync(request, config, cancellationToken).ConfigureAwait(false);

			if (!string.IsNullOrWhiteSpace(cacheKey))
			{
				RemoveFromCacheByCacheKey(cacheKey);
				if (!tokenResponse.HasError)
				{
					AddToCache(cacheKey, tokenResponse);
				}
			}
			
			return tokenResponse;
		}

		private Task<TokenResponse> RequestRevokeTokenAsync(string providerName, RevokeTokenRequest request, CancellationToken cancellationToken)
		{
			var config = _tokenProviderConfigs.FirstOrDefault(o => o.ProviderName?.Equals(providerName, StringComparison.OrdinalIgnoreCase) ?? false);
			if (config == null) throw new InvalidOperationException($"No token provider is setup with provider name '{providerName}'.");

			string cacheKey;
			if (request.TokenType == TokenTypes.RefreshToken)
			{
				_cachedRefreshTokensKeys.TryGetValue(request.Token, out cacheKey);
				RemoveFromCacheByRefreshToken(request.Token);
			}
			else
			{
				_cachedAccessTokensKeys.TryGetValue(request.Token, out cacheKey);
				RemoveFromCacheByAccessToken(request.Token);
			}
			
			
			if (!string.IsNullOrWhiteSpace(cacheKey))
			{
				RemoveFromCacheByCacheKey(cacheKey);
			}

			return PrivateTokenRequestAsync(request, config, cancellationToken);
		}

		private async Task<TokenResponse> RequestTokenWithCachingAsync(string providerName, TokenRequestBase request, CancellationToken cancellationToken)
		{
			var config = _tokenProviderConfigs.FirstOrDefault(o => o.ProviderName?.Equals(providerName, StringComparison.OrdinalIgnoreCase) ?? false);
			if (config == null) throw new InvalidOperationException($"No token provider is setup with provider name '{providerName}'.");

			var cacheKey = request.BuildCacheKey(providerName, config);

			TokenResponse tokenResponse = null;
			if (!string.IsNullOrWhiteSpace(cacheKey))
			{
				if (_cachedTokenResponses.TryGetValue(cacheKey, out tokenResponse) && tokenResponse != null)
				{
					// cached and still not expired then return the token
					if (!tokenResponse.HasExpired) return tokenResponse;

					RemoveFromCacheByCacheKey(cacheKey);
					tokenResponse.AccessToken = string.Empty;

					// if refresh token exists then get new token using refresh token 
					if (!string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
					{
						tokenResponse = await PrivateRefreshTokenAsync(tokenResponse.RefreshToken, request, config, cancellationToken).ConfigureAwait(false);
					}
				}
			}
			
			// if tokenResponse is null or we still not have access token or token response has error then try to get a new token
			if (string.IsNullOrWhiteSpace(tokenResponse?.AccessToken) || tokenResponse.HasInvalidTokenError)
			{
				tokenResponse = await PrivateTokenRequestAsync(request, config, cancellationToken).ConfigureAwait(false);
			}

			if (tokenResponse.HasError)
			{
				RemoveFromCacheByCacheKey(cacheKey);
			}
			else
			{
				AddToCache(cacheKey, tokenResponse);
			}
			return tokenResponse;
		}

		private async Task<TokenResponse> SendHttpRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			try
			{
				using (var response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false))
				{
					var tokenResponse = new TokenResponse();
					string content = null;
					try
					{
						content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
						if (!string.IsNullOrWhiteSpace(content))
						{
							try
							{
								tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);
							}
							catch (Exception e)
							{
								tokenResponse.RawResponse = content;
								tokenResponse.Exception = e;
								tokenResponse.ErrorDescription = e.Message;
							}
						}
					}
					catch { }

					tokenResponse.RawResponse = content;

					if (!response.IsSuccessStatusCode && string.IsNullOrWhiteSpace(tokenResponse.ErrorDescription))
					{
						tokenResponse.ErrorDescription = response.ReasonPhrase;
					}
					
					return tokenResponse;
				}
			}
			catch (Exception exception)
			{
				return new TokenResponse
				{
					Exception = exception,
					ErrorDescription = exception.Message
				};
			}
		}

		private async Task<TokenResponse> PrivateTokenRequestAsync(TokenRequestBase request, TokenProviderConfig config, CancellationToken cancellationToken)
		{
			using (var requestMessage = request.BuildRequestTokenHttpRequestMessage(config))
			{
				return await SendHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);
			}
		}

		private Task<TokenResponse> PrivateRefreshTokenAsync(string refreshToken, TokenRequestBase request, TokenProviderConfig config, CancellationToken cancellationToken)
		{
			return PrivateTokenRequestAsync(new RefreshTokenRequest(request, refreshToken), config, cancellationToken);
		}

		private void AddToCache(string cacheKey, TokenResponse tokenResponse)
		{
			if (string.IsNullOrWhiteSpace(cacheKey)) return;

			if (tokenResponse == null) return;
			if (!string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
			{
				_cachedTokenResponses.TryAdd(cacheKey, tokenResponse);

				// for removing from cache when revoking token
				_cachedAccessTokensKeys.TryAdd(tokenResponse.AccessToken, cacheKey);
			}

			// for removing from cache when refreshing token
			if (!string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
			{
				_cachedRefreshTokensKeys.TryAdd(tokenResponse.RefreshToken, cacheKey);
			}
		}

		private void RemoveFromCacheByCacheKey(string cacheKey)
		{
			if (string.IsNullOrWhiteSpace(cacheKey)) return;

			if (_cachedTokenResponses.TryRemove(cacheKey, out TokenResponse tokenResponse) && tokenResponse != null)
			{
				// also remove from access and refresh token cache stores
				if (!string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
					_cachedAccessTokensKeys.TryRemove(tokenResponse.AccessToken, out _);

				if (!string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
					_cachedRefreshTokensKeys.TryRemove(tokenResponse.RefreshToken, out _);
			}
		}

		private void RemoveFromCacheByRefreshToken(string refreshToken)
		{
			if (string.IsNullOrWhiteSpace(refreshToken)) return;

			if (_cachedRefreshTokensKeys.TryRemove(refreshToken, out string cacheKey))
			{
				RemoveFromCacheByCacheKey(cacheKey);
			}
		}

		private void RemoveFromCacheByAccessToken(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken)) return;

			if (_cachedAccessTokensKeys.TryRemove(accessToken, out string cacheKey))
			{
				RemoveFromCacheByCacheKey(cacheKey);
			}
		}

		public void Dispose()
		{
			_cachedTokenResponses?.Clear();
			_cachedAccessTokensKeys?.Clear();
			_cachedRefreshTokensKeys?.Clear();
		}
	}
}
