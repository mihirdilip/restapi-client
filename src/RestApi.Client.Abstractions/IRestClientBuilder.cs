// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;
using System;
using System.ComponentModel;

namespace RestApi.Client
{
	public interface IRestClientBuilder
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		IServiceCollection Services { get; }

		IRestClientBuilder SetBaseAddress(Uri baseAddress);

		IRestClientBuilder SetDefaultRequestHeaders(RestHttpHeaders defaultRequestHeaders);

		IRestClientBuilder SetMaxResponseContentBufferSize(int maxResponseContentBufferSize);

		IRestClientBuilder SetTimeout(TimeSpan timeout);

		IRestClientBuilder SetRestClientOptions(RestClientOptions options);

		IRestClientBuilder AddHttpContentSerializer<THttpContentSerializerImplementation>() 
			where THttpContentSerializerImplementation : class, IHttpContentSerializer;

		IRestClientBuilder ClearHttpContentSerializers();

		IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandlerImplementation>()
			where TRestAuthenticationHandlerImplementation : class, IRestAuthenticationHandler;

		IRestClientBuilder AddValidator<TRestClientValidatorImplementation>()
			where TRestClientValidatorImplementation : class, IRestClientValidator;

		IRestClientBuilder ClearAuthenticationHandler();

		IRestClientBuilder ReplaceRestClient<TRestClientImplementation>()
			where TRestClientImplementation : class, IRestClient;

		IRestClient Build();
	}
}