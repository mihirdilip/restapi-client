// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

namespace RestApi.Client
{
	/// <summary>
	/// A fluent builder for building <see cref="IRestClient"/> which is the main core of this library.
	/// <para>Plain Text and Json http content serializers <see cref="IHttpContentSerializer"/> are added by default.</para>
	/// </summary>
	public class RestClientBuilder : IRestClientBuilder
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IServiceCollection Services { get; }

		/// <summary>
		/// Creates an instance of rest client builder.
		/// </summary>
		public RestClientBuilder()
			: this(new RestClientOptions())
		{
		}

		/// <summary>
		/// Creates an instance of rest client builder with base uri and optionally default request headers <see cref="RestHttpHeaders"/> to be used for all the requests made by the <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="baseAddress">The base uri to be used for all the requests.</param>
		/// <param name="defaultRequestHeaders">The default request headers <see cref="RestHttpHeaders"/> to be used for all the requests.</param>
		public RestClientBuilder(Uri baseAddress, RestHttpHeaders defaultRequestHeaders = null)
			: this(new RestClientOptions { BaseAddress = baseAddress, DefaultRequestHeaders = defaultRequestHeaders })
		{
		}

		/// <summary>
		/// Creates an instance of rest client builder with <see cref="RestClientOptions"/>.
		/// </summary>
		/// <param name="options">The <see cref="RestClientOptions"/> used by the builder for building <see cref="IRestClient"/>.</param>
		public RestClientBuilder(RestClientOptions options)
			: this (new ServiceCollection(), options)
		{
		}

		internal RestClientBuilder(IServiceCollection services, RestClientOptions options = null)
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

		/// <summary>
		/// Set the <see cref="RestClientOptions"/>.
		/// </summary>
		/// <param name="options">The <see cref="RestClientOptions"/> used by the builder for building <see cref="IRestClient"/>.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetRestClientOptions(RestClientOptions options)
		{
			if (options == null) return this;

			return SetBaseAddress(options.BaseAddress)
				.SetDefaultRequestHeaders(options.DefaultRequestHeaders);
		}

		/// <summary>
		/// Sets the base uri to be used for all the requests made by the <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="baseAddress">The base uri</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetBaseAddress(Uri baseAddress)
		{
			Services.Configure<RestClientOptions>(options => options.BaseAddress = baseAddress);
			return this;
		}

		/// <summary>
		/// Sets the default request headers <see cref="RestHttpHeaders"/> to be used for all the requests made by the <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="defaultRequestHeaders">The default request headers.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetDefaultRequestHeaders(RestHttpHeaders defaultRequestHeaders)
		{
			Services.Configure<RestClientOptions>(options => options.DefaultRequestHeaders = defaultRequestHeaders);
			return this;
		}

		/// <summary>
		/// Sets the maximum response content buffer size on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="maxResponseContentBufferSize">The maximum response content buffer size.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetMaxResponseContentBufferSize(int maxResponseContentBufferSize)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpClientActions.Add(client => client.MaxResponseContentBufferSize = maxResponseContentBufferSize));
			return this;
		}

		/// <summary>
		/// Sets the request timeout on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetTimeout(TimeSpan timeout)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpClientActions.Add(client => client.Timeout = timeout));
			return this;
		}

		/// <summary>
		/// Sets the primary <see cref="HttpMessageHandler"/> on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="primaryHandler">The primary <see cref="HttpMessageHandler"/>.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder SetPrimaryHttpMessageHandler(HttpMessageHandler primaryHandler)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpMessageHandlerBuilderActions.Add(builder => builder.PrimaryHandler = primaryHandler));
			return this;
		}

		/// <summary>
		/// Adds an additional <see cref="DelegatingHandler"/> on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="additionalHandler">An additional <see cref="DelegatingHandler"/>.</param>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder AddAdditionalDelegatingHandler(DelegatingHandler additionalHandler)
		{
			Services.Configure<HttpClientFactoryOptions>(c => c.HttpMessageHandlerBuilderActions.Add(builder => builder.AdditionalHandlers.Add(additionalHandler)));
			return this;
		}

		/// <summary>
		/// Adds your custom http content serializer <see cref="IHttpContentSerializer"/> to the pipeline as singleton.
		/// <para>Common content serializers available to download as NuGet packages (RestApi.Client.ContentSerializer.*) which can be added to the pipeline.</para>
		/// Content serializer takes care of the request/response content serialization depending on the content type.
		/// </summary>
		/// <typeparam name="THttpContentSerializerImplementation">Your custom http content serializer which implements <see cref="IHttpContentSerializer"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder AddHttpContentSerializer<THttpContentSerializerImplementation>()
			where THttpContentSerializerImplementation : class, IHttpContentSerializer
		{
			Services.AddSingleton<IHttpContentSerializer, THttpContentSerializerImplementation>();
			return this;
		}

		/// <summary>
		/// Clears all the http content serializers <see cref="IHttpContentSerializer"/> from the pipeline.
		/// <para>Content serializer takes care of the request/response content serialization depending on the content type.</para>
		/// </summary>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder ClearHttpContentSerializers()
		{
			Services.RemoveAll<IHttpContentSerializer>();
			return this;
		}

		/// <summary>
		/// Adds your custom authentication handler <see cref="IRestAuthenticationHandler"/> to the pipeline as singleton or replaces the exiting authentication handler if already added before.
		/// A <see cref="IRestClient"/> can only have one authentication handler at a time.
		/// <para>Common authentication handlers are available to download as NuGet packages (RestApi.Client.Authentication.*) which can be added to the pipeline.</para>
		/// Authentication handler takes care of the authenticating all the request sent by the <see cref="IRestClient"/>.
		/// </summary>
		/// <typeparam name="TRestAuthenticationHandlerImplementation">Your custom authentication handler which implements <see cref="IRestAuthenticationHandler"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandlerImplementation>()
			where TRestAuthenticationHandlerImplementation : class, IRestAuthenticationHandler
		{
			ClearAuthenticationHandler();
			Services.AddSingleton<IRestAuthenticationHandler, TRestAuthenticationHandlerImplementation>();
			return this;
		}

		/// <summary>
		/// Clears any authentication handler <see cref="IRestAuthenticationHandler"/> from the pipeline.
		/// Content serializer takes care of the request/response content serialization depending on the content type.
		/// </summary>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder ClearAuthenticationHandler()
		{
			Services.RemoveAll<IRestAuthenticationHandler>();
			return this;
		}

		/// <summary>
		/// Adds an implementation of <see cref="IRestClientValidator"/> which validates an instance of <see cref="IRestClient"/> on creation.
		/// </summary>
		/// <typeparam name="TRestClientValidatorImplementation">Your validator which implements <see cref="IRestClientValidator"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder AddValidator<TRestClientValidatorImplementation>() 
			where TRestClientValidatorImplementation : class, IRestClientValidator
		{
			Services.AddSingleton<IRestClientValidator, TRestClientValidatorImplementation>();
			return this;
		}

		/// <summary>
		/// Replaces the default implementation of <see cref="IRestClient"/> with your custom implementation and adds to the dependency container as a singleton.
		/// </summary>
		/// <typeparam name="TRestClientImplementation">Your custom rest client which implements <see cref="IRestClient"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		public IRestClientBuilder ReplaceRestClient<TRestClientImplementation>() 
			where TRestClientImplementation : class, IRestClient
		{
			Services.RemoveAll<IRestClient>();
			Services.AddSingleton<IRestClient, TRestClientImplementation>();
			return this;
		}

		/// <summary>
		/// Builds and returns a singleton instance of <see cref="IRestClient"/> which is the main core of this library and is used for all the rest api requests.
		/// </summary>
		/// <returns>Singleton instance of <see cref="IRestClient"/>.</returns>
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