// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Net.Http;

namespace RestApi.Client.Authentication
{
	public static class WindowsExtensions
	{
		public static IRestClientBuilder AddWindowsDefaultAuthentication(this IRestClientBuilder builder)
		{
			builder.Services.Configure<HttpClientFactoryOptions>(
				c => c.HttpMessageHandlerBuilderActions.Add(b =>
				{
					if (b.PrimaryHandler is HttpClientHandler clientHandler)
					{
						clientHandler.UseDefaultCredentials = true;
					}
					else
					{
						b.PrimaryHandler = new HttpClientHandler
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
