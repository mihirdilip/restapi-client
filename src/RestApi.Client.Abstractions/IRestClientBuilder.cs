// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using RestApi.Client.Authentication;
using RestApi.Client.ContentSerializer;

namespace RestApi.Client
{
	public interface IRestClientBuilder
	{
		IServiceCollection Services { get; }

		IRestClientBuilder AddHttpContentSerializer<TSerializer>() 
			where TSerializer : class, IHttpContentSerializer;

		IRestClientBuilder ClearHttpContentSerializers();

		IRestClientBuilder AddAuthenticationHandler<TRestAuthenticationHandler>()
			where TRestAuthenticationHandler : class, IRestAuthenticationHandler;

		IRestClientBuilder ClearAuthenticationHandler();

		IRestClient Build();
	}
}