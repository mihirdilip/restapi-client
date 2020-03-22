// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace RestApi.Client
{
	public static class RestClientServiceCollectionExtensions
	{
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress), null);
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress), configure);
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, RestClientOptions options = null)
		{
			return AddRestClient(services, options, null);
		}

		public static IServiceCollection AddRestClient(this IServiceCollection services, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(), configure);
		}

		internal static IServiceCollection AddRestClient(this IServiceCollection services, RestClientOptions options, Action<IRestClientBuilder> configure)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			var builder = new RestClientBuilder(services, options);
			configure?.Invoke(builder);
			return services;
		}
	}
}
