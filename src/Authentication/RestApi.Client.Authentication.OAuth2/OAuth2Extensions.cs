// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class OAuth2Extensions
	{
		/// <summary>
		/// Adds OAuth 2.0 authentication handling to the pipeline. This will send the bearer token in the Authorization header of a request.
		/// </summary>
		/// <typeparam name="TOAuth2AuthenticationProvider">Implementation type inherited from <see cref="IOAuth2AuthenticationProvider"/>.</typeparam>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddOAuth2Authentication<TOAuth2AuthenticationProvider>(this IRestClientBuilder builder)
			where TOAuth2AuthenticationProvider : class, IOAuth2AuthenticationProvider
		{
			builder.Services.AddSingleton<IOAuth2AuthenticationProvider, TOAuth2AuthenticationProvider>();
			builder.AddAuthenticationHandler<OAuth2AuthenticationHandler>();
			return builder;
		}
	}
}
