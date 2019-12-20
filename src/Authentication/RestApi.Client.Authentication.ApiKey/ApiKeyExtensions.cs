// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class ApiKeyExtensions
	{
		public static IRestClientBuilder AddApiKeyInHeaderAuthentication<TApiKeyAuthenticationProvider>(this IRestClientBuilder builder)
			where TApiKeyAuthenticationProvider : class, IApiKeyAuthenticationProvider
		{
			builder.Services.AddScoped<IApiKeyAuthenticationProvider, TApiKeyAuthenticationProvider>();
			builder.AddAuthenticationHandler<ApiKeyInHeaderAuthenticationHandler>();
			return builder;
		}

		public static IRestClientBuilder AddApiKeyInQueryParamsAuthentication<TApiKeyAuthenticationProvider>(this IRestClientBuilder builder)
			where TApiKeyAuthenticationProvider : class, IApiKeyAuthenticationProvider
		{
			builder.Services.AddScoped<IApiKeyAuthenticationProvider, TApiKeyAuthenticationProvider>();
			builder.AddAuthenticationHandler<ApiKeyInQueryParamsAuthenticationHandler>();
			return builder;
		}
	}
}
