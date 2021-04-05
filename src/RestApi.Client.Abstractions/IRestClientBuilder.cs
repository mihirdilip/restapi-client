// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;
using System;
using System.ComponentModel;
using System.Net.Http;

namespace RestApi.Client
{
	/// <summary>
	/// An interface for a fluent builder for building <see cref="IRestClient"/> which is the main core of this library.
	/// <para>Plain Text and Json http content serializers <see cref="IHttpContentSerializer"/> are added by default.</para>
	/// </summary>
	public interface IRestClientBuilder
	{
		/// <summary>
		/// It is for internal use only. You should use your service collection available on your pipeline.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		IServiceCollection Services { get; }

		string Name { get; }

		/// <summary>
		/// Set the <see cref="RestClientOptions"/>.
		/// </summary>
		/// <param name="options">The <see cref="RestClientOptions"/> used by the builder for building <see cref="IRestClient"/>.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetRestClientOptions(RestClientOptions options);

		/// <summary>
		/// Sets the base uri to be used for all the requests made by the <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="baseAddress">The base uri</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetBaseAddress(Uri baseAddress);

		/// <summary>
		/// Sets the default request headers <see cref="RestHttpHeaders"/> to be used for all the requests made by the <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="defaultRequestHeaders">The default request headers.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetDefaultRequestHeaders(RestHttpHeaders defaultRequestHeaders);

		/// <summary>
		/// Sets the maximum response content buffer size on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="maxResponseContentBufferSize">The maximum response content buffer size.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetMaxResponseContentBufferSize(int maxResponseContentBufferSize);

		/// <summary>
		/// Sets the request timeout on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetTimeout(TimeSpan timeout);

		/// <summary>
		/// Sets the primary <see cref="HttpMessageHandler"/> on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="primaryHandler">The primary <see cref="HttpMessageHandler"/>.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder SetPrimaryHttpMessageHandler(HttpMessageHandler primaryHandler);

		/// <summary>
		/// Adds an additional <see cref="DelegatingHandler"/> on the <see cref="HttpClient"/> used by the <see cref="RestClient"/> internally.
		/// </summary>
		/// <param name="additionalHandler">An additional <see cref="DelegatingHandler"/>.</param>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder AddAdditionalDelegatingHandler(DelegatingHandler additionalHandler);

		/// <summary>
		/// Adds your custom http content serializer <see cref="IHttpContentSerializer"/> to the pipeline.
		/// <para>Common content serializers available to download as NuGet packages (RestApi.Client.ContentSerializer.*) which can be added to the pipeline.</para>
		/// Content serializer takes care of the request/response content serialization depending on the content type.
		/// </summary>
		/// <typeparam name="THttpContentSerializerImplementation">Your custom http content serializer which implements <see cref="IHttpContentSerializer"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder AddHttpContentSerializer<THttpContentSerializerImplementation>() 
			where THttpContentSerializerImplementation : class, IHttpContentSerializer;

		/// <summary>
		/// Clears all the http content serializers <see cref="IHttpContentSerializer"/> from the pipeline.
		/// <para>Content serializer takes care of the request/response content serialization depending on the content type.</para>
		/// </summary>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder ClearHttpContentSerializers();

		/// <summary>
		/// Adds your custom authentication handler <see cref="IRestAuthenticationHandler"/> to the pipeline or replaces the exiting authentication handler if already added before.
		/// A <see cref="IRestClient"/> can only have one authentication handler at a time.
		/// <para>Common authentication handlers are available to download as NuGet packages (RestApi.Client.Authentication.*) which can be added to the pipeline.</para>
		/// Authentication handler takes care of the authenticating all the request sent by the <see cref="IRestClient"/>.
		/// </summary>
		/// <typeparam name="TRestAuthenticationHandlerImplementation">Your custom authentication handler which implements <see cref="IRestAuthenticationHandler"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandlerImplementation>()
			where TRestAuthenticationHandlerImplementation : class, IRestAuthenticationHandler;

		/// <summary>
		/// Clears any authentication handler <see cref="IRestAuthenticationHandler"/> from the pipeline.
		/// Content serializer takes care of the request/response content serialization depending on the content type.
		/// </summary>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder ClearAuthenticationHandler();

		/// <summary>
		/// Adds an implementation of <see cref="IRestClientValidator"/> which validates an instance of <see cref="IRestClient"/> on creation.
		/// </summary>
		/// <typeparam name="TRestClientValidatorImplementation">Your validator which implements <see cref="IRestClientValidator"/>.</typeparam>
		/// <returns>Current rest client builder.</returns>
		IRestClientBuilder AddValidator<TRestClientValidatorImplementation>()
			where TRestClientValidatorImplementation : class, IRestClientValidator;

		/// <summary>
		/// Builds and returns a singleton instance of <see cref="IRestClient"/> which is the main core of this library and is used for all the rest api requests.
		/// </summary>
		/// <returns>Singleton instance of <see cref="IRestClient"/>.</returns>
		IRestClient Build();
	}
}