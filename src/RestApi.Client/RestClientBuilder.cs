// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;

namespace RestApi.Client
{
	public class RestClientBuilder : IRestClientBuilder
	{
		public IServiceCollection Services { get; }

		public RestClientBuilder()
			: this (new ServiceCollection())
		{
		}

		internal RestClientBuilder(IServiceCollection services)
		{
			Services = services;

			services.AddOptions();
			services.AddLogging(builder => builder.AddConsole().AddDebug());
			services.AddHttpClient();

			services.AddSingleton<IHttpContentHandler, HttpContentHandler>();
			services.AddSingleton<IRestClient, RestClient>();

			this.AddPlainTextHttpContentSerializer();
			this.AddJsonHttpContentSerializer();
			this.AddNullAuthentication();
		}

		public IRestClientBuilder AddHttpContentSerializer<THttpContentSerializer>()
			where THttpContentSerializer : class, IHttpContentSerializer
		{
			Services.AddSingleton<IHttpContentSerializer, THttpContentSerializer>();
			return this;
		}

		public IRestClientBuilder ClearHttpContentSerializers()
		{
			Services.RemoveAll<IHttpContentSerializer>();
			return this;
		}

		public IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandler>()
			where TRestAuthenticationHandler : class, IRestAuthenticationHandler
		{
			ClearAuthenticationHandler();
			Services.AddScoped<IRestAuthenticationHandler, TRestAuthenticationHandler>();
			return this;
		}

		public IRestClientBuilder ClearAuthenticationHandler()
		{
			Services.RemoveAll<IRestAuthenticationHandler>();
			return this;
		}

		public IRestClient Build()
		{
			return Services.BuildServiceProvider().GetService<IRestClient>();
		}
	}
}