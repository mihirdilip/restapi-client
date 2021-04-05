// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestApi.Client.ContentSerializer;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApi.Client.Internals
{
	internal class RestClientFactory : IRestClientFactory
	{
		private readonly IOptionsMonitor<RestClientOptions> _optionsMonitor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IServiceProvider _serviceProvider;

		public RestClientFactory(IOptionsMonitor<RestClientOptions> optionsMonitor, IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
		{
			_optionsMonitor = optionsMonitor;
			_httpClientFactory = httpClientFactory;
			_serviceProvider = serviceProvider;
		}

		public IRestClient CreateClient(string name)
		{
			if (name == null) name = string.Empty;

			var options = _optionsMonitor.Get(name);
			if (options == null) throw new InvalidOperationException($"No rest client with name '{name}' is added to the startup builder.");

			var httpClient = _httpClientFactory.CreateClient(name);
			var httpContentHandler = new HttpContentHandler(GetHttpContentSerializers(options));

			var client = new RestClient(options, httpClient, httpContentHandler, _serviceProvider);
			RunValidations(options);
			return client;
		}

		public TClient CreateClient<TClient>(string name = null) 
			where TClient : class
		{
			if (string.IsNullOrWhiteSpace(name)) name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), false);

			var client = CreateClient(name);
			return (TClient)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TClient), new object[] { client });
		}

		public TClient CreateClient<TClient, TImplementation>(string name)
			where TClient : class
			where TImplementation : class, TClient
		{
			if (string.IsNullOrWhiteSpace(name)) name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), false);

			var client = CreateClient(name);
			return (TClient)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TImplementation), new object[] { client });
		}

		private IEnumerable<IHttpContentSerializer> GetHttpContentSerializers(RestClientOptions options)
		{
			var httpContentSerializers = options.HttpContentSerializers;
			options.HttpContentSerializerImplementationTypes.ForEach(
				i => httpContentSerializers.Add(
					(IHttpContentSerializer) ActivatorUtilities.CreateInstance(_serviceProvider, i)
				)
			);
			return httpContentSerializers;
		}

		private void RunValidations(RestClientOptions options)
		{
			options.RestClientValidators.ForEach(i => i.Validate());
			options.RestClientValidatorImplementationTypes.ForEach(
				i => ((IRestClientValidator)ActivatorUtilities.CreateInstance(_serviceProvider, i))
						.Validate()
			);
		}
	}
}
