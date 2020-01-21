// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class OAuth2Extensions
	{
		public static IRestClientBuilder AddOAuth2Authentication<TOAuth2AuthenticationProvider>(this IRestClientBuilder builder)
			where TOAuth2AuthenticationProvider : class, IOAuth2AuthenticationProvider
		{
			builder.Services.AddScoped<IOAuth2AuthenticationProvider, TOAuth2AuthenticationProvider>();
			builder.AddAuthenticationHandler<OAuth2AuthenticationHandler>();
			return builder;
		}
	}
}
