// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;
using System;
using System.Linq;

namespace RestApi.Client
{
	public class RestClientBuilder : IRestClientBuilder
	{
		public IServiceCollection Services { get; }

		public RestClientBuilder()
			: this(new RestClientOptions())
		{
		}

		public RestClientBuilder(Uri baseAddress, RestHttpHeaders defaultRequestHeaders = null)
			: this(new RestClientOptions { BaseAddress = baseAddress, DefaultRequestHeaders = defaultRequestHeaders })
		{
		}

		public RestClientBuilder(RestClientOptions options)
			: this (new ServiceCollection(), options)
		{
		}

		internal RestClientBuilder(IServiceCollection services, RestClientOptions options)
		{
			Services = services;

			Services.AddOptions();
			Services.AddLogging(builder => builder.AddConsole().AddDebug());
			Services.AddHttpClient();

			Services.AddSingleton<IHttpContentHandler, HttpContentHandler>();
			Services.AddSingleton<IRestClient, RestClient>();

			this.AddPlainTextHttpContentSerializer();
			this.AddJsonHttpContentSerializer();
			this.AddNullAuthentication();

			SetRestClientOptions(options);
		}

		public IRestClientBuilder SetRestClientOptions(RestClientOptions options)
		{
			if (options == null) return this;

			return SetBaseAddress(options.BaseAddress)
				.SetDefaultRequestHeaders(options.DefaultRequestHeaders);
		}

		public IRestClientBuilder SetBaseAddress(Uri baseAddress)
		{
			Services.Configure<RestClientOptions>(options => options.BaseAddress = baseAddress);
			return this;
		}

		public IRestClientBuilder SetDefaultRequestHeaders(RestHttpHeaders defaultRequestHeaders)
		{
			Services.Configure<RestClientOptions>(options => options.DefaultRequestHeaders = defaultRequestHeaders);
			return this;
		}

		public IRestClientBuilder SetMaxResponseContentBufferSize(int maxResponseContentBufferSize)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpClientActions.Add(client => client.MaxResponseContentBufferSize = maxResponseContentBufferSize));
			return this;
		}

		public IRestClientBuilder SetTimeout(TimeSpan timeout)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpClientActions.Add(client => client.Timeout = timeout));
			return this;
		}

		public IRestClientBuilder AddHttpContentSerializer<THttpContentSerializerImplementation>()
			where THttpContentSerializerImplementation : class, IHttpContentSerializer
		{
			Services.AddSingleton<IHttpContentSerializer, THttpContentSerializerImplementation>();
			return this;
		}

		public IRestClientBuilder ClearHttpContentSerializers()
		{
			Services.RemoveAll<IHttpContentSerializer>();
			return this;
		}

		public IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandlerImplementation>()
			where TRestAuthenticationHandlerImplementation : class, IRestAuthenticationHandler
		{
			ClearAuthenticationHandler();
			Services.AddScoped<IRestAuthenticationHandler, TRestAuthenticationHandlerImplementation>();
			return this;
		}

		public IRestClientBuilder ClearAuthenticationHandler()
		{
			Services.RemoveAll<IRestAuthenticationHandler>();
			return this;
		}

		public IRestClientBuilder ReplaceRestClient<TRestClientImplementation>() 
			where TRestClientImplementation : class, IRestClient
		{
			Services.RemoveAll<IRestClient>();
			Services.AddSingleton<IRestClient, TRestClientImplementation>();
			return this;
		}

		public IRestClient Build()
		{
			if (1 != Services.Count(s => s.ServiceType == typeof(IRestAuthenticationHandler)))
			{
				throw new Exception("Only 1 authentication handler can be registered at a time.");
			}

			if (1 != Services.Count(s => s.ServiceType == typeof(IRestClient)))
			{
				throw new Exception("Only 1 rest client can be registered at a time.");
			}

			return Services.BuildServiceProvider().GetService<IRestClient>();
		}
	}
}