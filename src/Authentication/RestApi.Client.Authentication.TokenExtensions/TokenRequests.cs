// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestApi.Client.Authentication
{
    public interface ITokenRequest
    {
    }

    public abstract class TokenRequestBase : ITokenRequest
    {
	    protected TokenRequestBase()
	    {
		    
	    }

	    protected TokenRequestBase(TokenRequestBase other)
	    {
		    OverrideScope = other.OverrideScope;
            ExtraBodyParameters.Clear();
            foreach (var bodyParameter in other.ExtraBodyParameters)
            {
	            ExtraBodyParameters.Add(bodyParameter.Key, bodyParameter.Value);
            }
	    }

	    /// <summary>
	    /// Space separated list of the requested scope. This will override the default scope set when configuring the provider.
	    /// </summary>
	    public string OverrideScope { get; set; }

	    /// <summary>
	    /// Gets or sets extra parameters to be sent in the body of the request.
	    /// </summary>
	    public IDictionary<string, string> ExtraBodyParameters { get; set; } = new Dictionary<string, string>();

	    internal abstract string BuildCacheKey(string providerName, TokenProviderConfig config);
        internal abstract IDictionary<string, string> GetRequestSpecificParameters();

        internal HttpRequestMessage BuildRequestTokenHttpRequestMessage(TokenProviderConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.TokenRequestEndpointAddress)) throw new InvalidOperationException($"{nameof(config.TokenRequestEndpointAddress)} is not set on {config.GetType().Name}.");
            if (string.IsNullOrWhiteSpace(config.TokenRevocationEndpointAddress)) config.TokenRevocationEndpointAddress = config.TokenRequestEndpointAddress;
            if (string.IsNullOrWhiteSpace(config.ClientId)) throw new InvalidOperationException($"{nameof(config.ClientId)} is not set on {config.GetType().Name}.");
            if (config.ClientSecret == null) config.ClientSecret = string.Empty;

            var bodyParameters = GetRequestSpecificParameters() ?? new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(OverrideScope))
            {
                bodyParameters.Add(OidcConstants.TokenRequest.Scope, OverrideScope);
            }
            else if (!string.IsNullOrWhiteSpace(config.Scope))
            {
                bodyParameters.Add(OidcConstants.TokenRequest.Scope, config.Scope);
            }

            if (ExtraBodyParameters != null)
            {
	            foreach (var param in ExtraBodyParameters)
	            {
		            if (bodyParameters.ContainsKey(param.Key)) continue;
		            bodyParameters.Add(param.Key, param.Value);
	            }
            }

            bodyParameters.Remove(OidcConstants.TokenRequest.ClientId);
            bodyParameters.Remove(OidcConstants.TokenRequest.ClientSecret);

            var uri = this is RevokeTokenRequest ? config.TokenRevocationEndpointAddress : config.TokenRequestEndpointAddress;
            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaMimeTypes.Application.Json));

            if (config.ClientAuthenticationMethod == ClientAuthenticationMethod.HeaderBasicAuthenticationOAuth2Spec)
            {
                message.Headers.Authorization = Utils.GetBasicAuthenticationOAuth2SpecHeaderValue(config.ClientId, config.ClientSecret);
            }
            else if (config.ClientAuthenticationMethod == ClientAuthenticationMethod.HeaderBasicAuthentication)
            {
                message.Headers.Authorization = Utils.GetBasicAuthenticationHeaderValue(config.ClientId, config.ClientSecret);
            }
            else
            {
                bodyParameters.Add(OidcConstants.TokenRequest.ClientId, config.ClientId);
                bodyParameters.Add(OidcConstants.TokenRequest.ClientSecret, config.ClientSecret);
            }

            bodyParameters.TryGetValue(OidcConstants.TokenRequest.GrantType, out var grantType);
            ValidateGrantType(grantType);

            message.Content = new FormUrlEncodedContent(bodyParameters);
            return message;
        }

        private void ValidateGrantType(string grantType)
        {
	        if (string.IsNullOrWhiteSpace(grantType) && this is RevokeTokenRequest)
	        {
		        return;
	        }

            if (string.IsNullOrWhiteSpace(grantType)) throw new ArgumentNullException(nameof(grantType));

	        if (grantType.Equals(OidcConstants.GrantTypes.Password, StringComparison.OrdinalIgnoreCase) && this is PasswordCredentialsTokenRequest)
	        {
                return;
	        }

	        if (grantType.Equals(OidcConstants.GrantTypes.ClientCredentials, StringComparison.OrdinalIgnoreCase) && this is ClientCredentialsTokenRequest)
	        {
		        return;
	        }

	        if (grantType.Equals(OidcConstants.GrantTypes.RefreshToken, StringComparison.OrdinalIgnoreCase) && this is RefreshTokenRequest)
	        {
		        return;
	        }

            throw new InvalidOperationException($"Grant Type set on {nameof(TokenProviderConfig)} does not match with the type of token request {GetType().Name}.");
        }
    }

    /// <summary>
    /// Request for token using client_credentials
    /// </summary>
    public class ClientCredentialsTokenRequest : TokenRequestBase
    {
        internal override string BuildCacheKey(string providerName, TokenProviderConfig config)
        {
	        return providerName + "-" + config.ClientId + "-" 
	               + (string.IsNullOrWhiteSpace(OverrideScope)
		               ? config.Scope?.Replace(" ", "-") ?? string.Empty
		               : OverrideScope.Replace(" ", "-")
	               );
        }

        internal override IDictionary<string, string> GetRequestSpecificParameters()
        {
	        return new Dictionary<string, string>
	        {
		        {OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.ClientCredentials}
	        };
        }
    }

    /// <summary>
    /// Request for token using password
    /// </summary>
    public class PasswordCredentialsTokenRequest : TokenRequestBase
    {
	    public PasswordCredentialsTokenRequest(string username, string password)
	    {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
            if (password == null) password = string.Empty;

            Username = username;
		    Password = password;
	    }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        internal string Username { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        internal string Password { get; }

        internal override string BuildCacheKey(string providerName, TokenProviderConfig config)
        {
	        return providerName + "-" + config.ClientId + "-" + Username + "-"
	               + (string.IsNullOrWhiteSpace(OverrideScope)
		               ? config.Scope?.Replace(" ", "-") ?? string.Empty
		               : OverrideScope.Replace(" ", "-")
	               );
        }

        internal override IDictionary<string, string> GetRequestSpecificParameters()
        {
	        return new Dictionary<string, string>
	        {
		        {OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.Password},
                {OidcConstants.TokenRequest.UserName, Username},
		        {OidcConstants.TokenRequest.Password, Password}
	        };
        }
    }

    /// <summary>
    /// Request for token using refresh_token.
    /// </summary>
    public class RefreshTokenRequest : TokenRequestBase
    {
	    public RefreshTokenRequest(string refreshToken)
	    {
            if (string.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

		    RefreshToken = refreshToken;
	    }

		internal RefreshTokenRequest(TokenRequestBase request, string refreshToken)
			: base (request)
		{
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

            RefreshToken = refreshToken;
		}

		/// <summary>
		/// Gets the refresh token.
		/// </summary>
		internal string RefreshToken { get; }

        internal override string BuildCacheKey(string providerName, TokenProviderConfig config)
        {
	        return RefreshToken;
        }

        internal override IDictionary<string, string> GetRequestSpecificParameters()
        {
	        return new Dictionary<string, string>
	        {
		        {OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.RefreshToken},
		        {OidcConstants.TokenRequest.RefreshToken, RefreshToken}
	        };
        }
    }

    /// <summary>
    /// Request to revoke a token.
    /// </summary>
    public class RevokeTokenRequest : TokenRequestBase
    {
	    public RevokeTokenRequest(string token, TokenTypes tokenType = TokenTypes.AccessToken)
	    {
		    if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

		    Token = token;
		    TokenType = tokenType;
	    }


	    /// <summary>
	    /// Gets the token.
	    /// </summary>
	    internal string Token { get; }

        internal TokenTypes TokenType { get; }

	    internal override string BuildCacheKey(string providerName, TokenProviderConfig config)
	    {
		    return Token;
	    }

	    internal override IDictionary<string, string> GetRequestSpecificParameters()
	    {
		    string tokenTypeHint;
		    switch (TokenType)
		    {
                case TokenTypes.AccessToken:
	                tokenTypeHint = OidcConstants.TokenTypes.AccessToken;
                    break;

                case TokenTypes.RefreshToken:
	                tokenTypeHint = OidcConstants.TokenTypes.RefreshToken;
	                break;

                case TokenTypes.IdentityToken:
	                tokenTypeHint = OidcConstants.TokenTypes.IdentityToken;
	                break;

                default:
	                tokenTypeHint = string.Empty;
                    break;
            }

		    return new Dictionary<string, string>
		    {
			    {OidcConstants.TokenIntrospectionRequest.Token, Token},
			    {OidcConstants.TokenIntrospectionRequest.TokenTypeHint, tokenTypeHint}
		    };
	    }
    }

    public enum TokenTypes
    {
        AccessToken,
        RefreshToken,
        IdentityToken
    }
}