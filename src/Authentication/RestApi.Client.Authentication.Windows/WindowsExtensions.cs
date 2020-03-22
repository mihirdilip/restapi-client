// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Net.Http;

namespace RestApi.Client.Authentication
{
	public static class WindowsExtensions
	{
		/// <summary>
		/// Adds windows authentication handling to the pipeline. This will add the credential of the current user logged on the Windows PC with the request.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddWindowsDefaultAuthentication(this IRestClientBuilder builder)
		{
			builder.Services.Configure<HttpClientFactoryOptions>(
				c => c.HttpMessageHandlerBuilderActions.Add(httpMessageHandlerBuilder =>
				{
					if (httpMessageHandlerBuilder.PrimaryHandler is HttpClientHandler clientHandler)
					{
						clientHandler.UseDefaultCredentials = true;
					}
					else
					{
						httpMessageHandlerBuilder.PrimaryHandler = new HttpClientHandler
						{
							UseDefaultCredentials = true
						};
					}
				})
			);

			return builder;
		}
	}
}
