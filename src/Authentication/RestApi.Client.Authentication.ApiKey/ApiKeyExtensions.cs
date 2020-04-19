// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class ApiKeyExtensions
	{
		/// <summary>
		/// Adds API Key authentication handling to the pipeline. This will send the API Key in the header of a request.
		/// </summary>
		/// <typeparam name="TApiKeyAuthenticationProvider">Implementation type inherited from <see cref="IApiKeyAuthenticationProvider"/>.</typeparam>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddApiKeyInHeaderAuthentication<TApiKeyAuthenticationProvider>(this IRestClientBuilder builder)
			where TApiKeyAuthenticationProvider : class, IApiKeyAuthenticationProvider
		{
			builder.Services.AddSingleton<IApiKeyAuthenticationProvider, TApiKeyAuthenticationProvider>();
			builder.AddAuthenticationHandler<ApiKeyInHeaderAuthenticationHandler>();
			return builder;
		}

		/// <summary>
		/// Adds API Key authentication handling to the pipeline. This will send the API Key as query parameter on a request.
		/// </summary>
		/// <typeparam name="TApiKeyAuthenticationProvider">Implementation type inherited from <see cref="IApiKeyAuthenticationProvider"/>.</typeparam>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddApiKeyInQueryParamsAuthentication<TApiKeyAuthenticationProvider>(this IRestClientBuilder builder)
			where TApiKeyAuthenticationProvider : class, IApiKeyAuthenticationProvider
		{
			builder.Services.AddSingleton<IApiKeyAuthenticationProvider, TApiKeyAuthenticationProvider>();
			builder.AddAuthenticationHandler<ApiKeyInQueryParamsAuthenticationHandler>();
			return builder;
		}
	}
}
