// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class BearerExtensions
	{
		public static IRestClientBuilder AddBearerAuthentication<TBearerAuthenticationProvider>(this IRestClientBuilder builder)
			where TBearerAuthenticationProvider : class, IBearerAuthenticationProvider
		{
			builder.Services.AddScoped<IBearerAuthenticationProvider, TBearerAuthenticationProvider>();
			builder.AddAuthenticationHandler<BearerAuthenticationHandler>();
			return builder;
		}
	}
}
