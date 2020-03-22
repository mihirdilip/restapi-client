// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace RestApi.Client
{
	public static class RestClientServiceCollectionExtensions
	{
		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline with a base API address which will be used for all the requests.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="baseAddress">The base API address.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress), null);
		}

		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline with a base API address which will be used for all the requests.
		/// Also use the <see cref="IRestClientBuilder"/> for configuring the <see cref="IRestClient"/>.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="baseAddress">The base API address.</param>
		/// <param name="configure">The <see cref="IRestClientBuilder"/> for configuring <see cref="IRestClient"/>.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress), configure);
		}

		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline with a base API address and default request headers <see cref="RestHttpHeaders"/> which will be used for all the requests.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="baseAddress">The base API address.</param>
		/// <param name="defaultRequestHeaders">The default request headers <see cref="RestHttpHeaders"/>.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}

		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline with a base API address and default request headers <see cref="RestHttpHeaders"/> which will be used for all the requests.
		/// Also use the <see cref="IRestClientBuilder"/> for configuring the <see cref="IRestClient"/>.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="baseAddress">The base API address.</param>
		/// <param name="defaultRequestHeaders">The default request headers <see cref="RestHttpHeaders"/>.</param>
		/// <param name="configure">The <see cref="IRestClientBuilder"/> for configuring <see cref="IRestClient"/>.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}

		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline.
		/// Also use the <see cref="IRestClientBuilder"/> for configuring the <see cref="IRestClient"/>.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The <see cref="IRestClientBuilder"/> for configuring <see cref="IRestClient"/>.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, new RestClientOptions(), configure);
		}

		private static IServiceCollection AddRestClient(this IServiceCollection services, RestClientOptions options, Action<IRestClientBuilder> configure)
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
