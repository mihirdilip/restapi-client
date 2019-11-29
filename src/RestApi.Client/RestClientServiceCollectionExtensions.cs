// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace RestApi.Client
{
	public static class RestClientServiceCollectionExtensions
	{
		public static IServiceCollection AddRestClient(this IServiceCollection services)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			var builder = new RestClientBuilder(services);
			return services;
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, Action<IRestClientBuilder> configure)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			configure?.Invoke(new RestClientBuilder(services));
			return services;
		}
	}
}
