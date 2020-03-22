// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public static class TokenExtensions
	{
		private static bool _addTokenProviderSetOnce = false;

		/// <summary>
		/// Adds a default token provider to the pipeline.
		/// This will be used as a default token provider by the pipeline when processing token request.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <param name="config">The <see cref="TokenProviderConfig"/>.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddTokenProvider(this IRestClientBuilder builder, TokenProviderConfig config)
		{
			return AddTokenProvider(builder, Constants.DefaultTokenProviderName, config);
		}

		/// <summary>
		/// Adds a token provider with a specific <paramref name="providerName"/> to the pipeline.
		/// This will be used as a token provider by the pipeline when processing token request with a specific <paramref name="providerName"/>.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <param name="providerName"></param>
		/// <param name="config">The <see cref="TokenProviderConfig"/>.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddTokenProvider(this IRestClientBuilder builder, string providerName, TokenProviderConfig config)
		{
			// set the internal provider name property on the config
			config.ProviderName = providerName;

			builder.Services.AddSingleton(config); // multiple entries possible but only single entry for a providerName which is validated in the builder validator.

			if (!_addTokenProviderSetOnce)
			{
				builder.Services.TryAddSingleton<ITokenService, TokenService>();
				builder.Services.TryAddSingleton<IRestTokenClient, RestTokenClient>();
				builder.AddValidator<TokenRestClientValidator>();

				_addTokenProviderSetOnce = true;
			}

			return builder;
		}

		/// <summary>
		/// Using the default token provider set up in the pipeline,
		/// it will process the <paramref name="request"/> of type
		/// <see cref="ClientCredentialsTokenRequest"/> or
		/// <see cref="PasswordCredentialsTokenRequest"/> or
		/// <see cref="RefreshTokenRequest"/> or
		/// <see cref="RevokeTokenRequest"/>
		/// and return the <see cref="TokenResponse"/>.
		/// <para>Note: If the code gives an error of circular dependency when using <see cref="IRestClient"/> for token request, try using <see cref="IRestTokenClient"/> instead.</para>
		/// </summary>
		/// <param name="client">The rest client.</param>
		/// <param name="request">
		/// <para><see cref="ClientCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with client credentials token.</para>
		/// <para><see cref="PasswordCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with password credentials token.</para>
		/// <para><see cref="RefreshTokenRequest"/> <paramref name="request"/> will refresh the token if supported by provider and return the <see cref="TokenResponse"/> with refreshed token.</para>
		/// <para><see cref="RevokeTokenRequest"/> <paramref name="request"/> will revoke the token from the provider.</para>
		/// </param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
		/// <returns>The <see cref="TokenResponse"/>.</returns>
		public static Task<TokenResponse> RequestTokenAsync(this IRestClient client, ITokenRequest request, CancellationToken cancellationToken = default)
		{
			return RequestTokenAsync(client, Constants.DefaultTokenProviderName, request, cancellationToken);
		}

		/// <summary>
		/// Using the specific token provider with <paramref name="providerName"/> set up in the pipeline,
		/// it will process the <paramref name="request"/> of type
		/// <see cref="ClientCredentialsTokenRequest"/> or
		/// <see cref="PasswordCredentialsTokenRequest"/> or
		/// <see cref="RefreshTokenRequest"/> or
		/// <see cref="RevokeTokenRequest"/>
		/// and return the <see cref="TokenResponse"/>.
		/// <para>Note: If the code gives an error of circular dependency when using <see cref="IRestClient"/> for token request, try using <see cref="IRestTokenClient"/> instead.</para>
		/// </summary>
		/// <param name="client">The rest client.</param>
		/// <param name="providerName">The token provider name.</param>
		/// <param name="request">
		/// <para><see cref="ClientCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with client credentials token.</para>
		/// <para><see cref="PasswordCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with password credentials token.</para>
		/// <para><see cref="RefreshTokenRequest"/> <paramref name="request"/> will refresh the token if supported by provider and return the <see cref="TokenResponse"/> with refreshed token.</para>
		/// <para><see cref="RevokeTokenRequest"/> <paramref name="request"/> will revoke the token from the provider.</para>
		/// </param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
		/// <returns>The <see cref="TokenResponse"/>.</returns>
		public static Task<TokenResponse> RequestTokenAsync(this IRestClient client, string providerName, ITokenRequest request, CancellationToken cancellationToken = default)
		{
			var tokenClient = client.ServiceProvider.GetService<IRestTokenClient>();
			return tokenClient.RequestTokenAsync(providerName, request, cancellationToken);
		}
	}
}
