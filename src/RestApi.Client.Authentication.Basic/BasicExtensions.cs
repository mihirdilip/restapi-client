// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class BasicExtensions
	{
		public static IRestClientBuilder AddBasicAuthentication<TBasicAuthenticationProvider>(this IRestClientBuilder builder)
			where TBasicAuthenticationProvider : class, IBasicAuthenticationProvider
		{
			builder.Services.AddScoped<IBasicAuthenticationProvider, TBasicAuthenticationProvider>();
			builder.AddAuthenticationHandler<BasicAuthenticationHandler>();
			return builder;
		}
	}
}
