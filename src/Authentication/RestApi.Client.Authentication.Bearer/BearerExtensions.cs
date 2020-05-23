// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class BearerExtensions
	{
		/// <summary>
		/// Adds Bearer token authentication handling to the pipeline as a singleton implementation. This will send the bearer token in the Authorization header of a request.
		/// </summary>
		/// <typeparam name="TBearerAuthenticationProvider">Implementation type inherited from <see cref="IBearerAuthenticationProvider"/>.</typeparam>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddBearerAuthentication<TBearerAuthenticationProvider>(this IRestClientBuilder builder)
			where TBearerAuthenticationProvider : class, IBearerAuthenticationProvider
		{
			builder.Services.AddSingleton<IBearerAuthenticationProvider, TBearerAuthenticationProvider>();
			builder.AddAuthenticationHandler<BearerAuthenticationHandler>();
			return builder;
		}
	}
}
