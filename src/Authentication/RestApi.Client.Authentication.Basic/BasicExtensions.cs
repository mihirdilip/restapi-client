// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Client.Authentication
{
	public static class BasicExtensions
	{
		/// <summary>
		/// Adds Basic authentication handling to the pipeline. This will send the Basic scheme authorization header with every request.
		/// </summary>
		/// <typeparam name="TBasicAuthenticationProvider">Implementation type inherited from <see cref="IBasicAuthenticationProvider"/>.</typeparam>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddBasicAuthentication<TBasicAuthenticationProvider>(this IRestClientBuilder builder)
			where TBasicAuthenticationProvider : class, IBasicAuthenticationProvider
		{
			builder.Services.AddSingleton<IBasicAuthenticationProvider, TBasicAuthenticationProvider>();
			builder.AddAuthenticationHandler<BasicAuthenticationHandler>();
			return builder;
		}
	}
}
