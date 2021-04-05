// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using RestApi.Client.Internals;
using System;
using System.Net.Http;

namespace RestApi.Client
{
	public static class RestClientServiceCollectionExtensions
	{
		public static IServiceCollection AddRestClient(this IServiceCollection services)
		{
			return AddRestClient(services, string.Empty, new RestClientOptions(), null);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services)
			where TClient : class
		{
			return AddRestClient<TClient>(services, string.Empty, new RestClientOptions(), null);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, string.Empty, new RestClientOptions(), null);
		}



		public static IServiceCollection AddRestClient(this IServiceCollection services, string name)
		{
			return AddRestClient(services, name, new RestClientOptions(), null);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(), null);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(), null);
		}




		/// <summary>
		/// Adds singleton <see cref="IRestClient"/> to the pipeline with a base API address which will be used for all the requests.
		/// It internally uses <see cref="HttpClient"/> for making API requests.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="baseAddress">The base API address.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRestClient(this IServiceCollection services, Uri baseAddress)
		{
			return AddRestClient(services, string.Empty, new RestClientOptions(baseAddress), null);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, Uri baseAddress)
			where TClient : class
		{
			return AddRestClient<TClient>(services, string.Empty, new RestClientOptions(baseAddress), null);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, string.Empty, new RestClientOptions(baseAddress), null);
		}




		public static IServiceCollection AddRestClient(this IServiceCollection services, string name, Uri baseAddress)
		{
			return AddRestClient(services, name, new RestClientOptions(baseAddress), null);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, Uri baseAddress)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(baseAddress), null);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, Uri baseAddress)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(baseAddress), null);
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
			return AddRestClient(services, string.Empty, new RestClientOptions(baseAddress), configure);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, Uri baseAddress, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, string.Empty, new RestClientOptions(baseAddress), configure);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, string.Empty, new RestClientOptions(baseAddress), configure);
		}




		public static IServiceCollection AddRestClient(this IServiceCollection services, string name, Uri baseAddress, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, name, new RestClientOptions(baseAddress), configure);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, Uri baseAddress, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(baseAddress), configure);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, Uri baseAddress, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(baseAddress), configure);
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
			return AddRestClient(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}
		
		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
			where TClient : class
		{
			return AddRestClient<TClient>(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}
		
		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}




		public static IServiceCollection AddRestClient(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
		{
			return AddRestClient(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}
		
		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
		}
		
		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), null);
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
			return AddRestClient(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}
		
		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}
		
		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, string.Empty, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}




		public static IServiceCollection AddRestClient(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, Uri baseAddress, RestHttpHeaders defaultRequestHeaders, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(baseAddress, defaultRequestHeaders), configure);
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
			return AddRestClient(services, null, new RestClientOptions(), configure);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, null, new RestClientOptions(), configure);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, null, new RestClientOptions(), configure);
		}



		public static IServiceCollection AddRestClient(this IServiceCollection services, string name, Action<IRestClientBuilder> configure)
		{
			return AddRestClient(services, name, new RestClientOptions(), configure);
		}

		public static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			return AddRestClient<TClient>(services, name, new RestClientOptions(), configure);
		}

		public static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			return AddRestClient<TClient, TImplementation>(services, name, new RestClientOptions(), configure);
		}





		private static IServiceCollection AddRestClient(this IServiceCollection services, string name, RestClientOptions options, Action<IRestClientBuilder> configure)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (name == null) name = string.Empty;
			if (options == null) options = new RestClientOptions();

			services.Configure<RestClientOptions>(name, o => o.CopyFrom(options));

			var builder = new RestClientBuilder(services, name);
			if (!string.IsNullOrWhiteSpace(name)) builder.AddNamedClient(name);
			configure?.Invoke(builder);
			return services;
		}

		private static IServiceCollection AddRestClient<TClient>(this IServiceCollection services, string name, RestClientOptions options, Action<IRestClientBuilder> configure)
			where TClient : class
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (string.IsNullOrWhiteSpace(name)) name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), false);
			if (options == null) options = new RestClientOptions();

			services.Configure<RestClientOptions>(name, o => o.CopyFrom(options));

			var builder = new RestClientBuilder(services, name);
			builder.AddTypedClient<TClient>(name);
			configure?.Invoke(builder);
			return services;
		}

		private static IServiceCollection AddRestClient<TClient, TImplementation>(this IServiceCollection services, string name, RestClientOptions options, Action<IRestClientBuilder> configure)
			where TClient : class
			where TImplementation : class, TClient
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (string.IsNullOrWhiteSpace(name)) name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), false);
			if (options == null) options = new RestClientOptions();

			services.Configure<RestClientOptions>(name, o => o.CopyFrom(options));

			var builder = new RestClientBuilder(services, name);
			builder.AddTypedClient<TClient, TImplementation>(name);
			configure?.Invoke(builder);
			return services;
		}
	}
}
