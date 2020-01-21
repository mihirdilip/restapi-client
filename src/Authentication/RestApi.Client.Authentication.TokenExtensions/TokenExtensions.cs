// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public static class TokenExtensions
	{
		private static bool _addTokenProviderSetOnce = false;

		public static IRestClientBuilder AddTokenProvider(this IRestClientBuilder builder, TokenProviderConfig config)
		{
			return AddTokenProvider(builder, Constants.DefaultTokenProviderName, config);
		}

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

		public static Task<TokenResponse> RequestTokenAsync(this IRestClient client, ITokenRequest request)
		{
			return RequestTokenAsync(client, Constants.DefaultTokenProviderName, request);
		}

		public static Task<TokenResponse> RequestTokenAsync(this IRestClient client, string providerName, ITokenRequest request)
		{
			var tokenClient = client.ServiceProvider.GetService<IRestTokenClient>();
			return tokenClient.RequestTokenAsync(providerName, request);
		}
	}
}
