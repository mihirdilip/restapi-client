// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestApi.Client.Authentication;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client
{
	internal class RestClient : IRestClient
	{
		private readonly RestClientOptions _options;
		private readonly HttpClient _httpClient;
		private readonly IHttpContentHandler _bodyContentHandler;
		private readonly IRestAuthenticationHandler _restAuthenticationHandler;

		public RestClient(IOptions<RestClientOptions> options, IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory, IHttpContentHandler bodyContentHandler, IRestAuthenticationHandler restAuthenticationHandler)
		{
			_options = options.Value;
			_httpClient = httpClientFactory.CreateClient();
			ServiceProvider = serviceProvider;
			_bodyContentHandler = bodyContentHandler;
			_restAuthenticationHandler = restAuthenticationHandler;

			ValidateInstance();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public IServiceProvider ServiceProvider { get; }

		public Task<IRestResponse> GetAsync(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Get, url, cancellationToken);
		}

		public Task<IRestResponse> GetAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Get, url, headers, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent>(HttpMethod.Get, url, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent>(HttpMethod.Get, url, headers, cancellationToken);
		}





		public Task<IRestResponse> PostAsync(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Post, url, cancellationToken);
		}

		public Task<IRestResponse> PostAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Post, url, headers, cancellationToken);
		}

		public Task<IRestResponse> PostAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Post, url, content, cancellationToken);
		}

		public Task<IRestResponse> PostAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Post, url, content, headers, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Post, url, content, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Post, url, content, headers, cancellationToken);
		}






		public Task<IRestResponse> PutAsync(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Put, url, cancellationToken);
		}

		public Task<IRestResponse> PutAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Put, url, headers, cancellationToken);
		}

		public Task<IRestResponse> PutAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Put, url, content, cancellationToken);
		}

		public Task<IRestResponse> PutAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Put, url, content, headers, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Put, url, content, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Put, url, content, headers, cancellationToken);
		}





		public Task<IRestResponse> DeleteAsync(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Delete, url, cancellationToken);
		}

		public Task<IRestResponse> DeleteAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync(HttpMethod.Delete, url, headers, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent>(HttpMethod.Delete, url, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent>(HttpMethod.Delete, url, headers, cancellationToken);
		}






		public Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default)
		{
			return SendAsync<object>(httpMethod, url, default, default, cancellationToken);
		}

		public Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<object>(httpMethod, url, default, headers, cancellationToken);
		}

		public Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync(httpMethod, url, content, default, cancellationToken);
		}

		public async Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			using (var request = await BuildHttpRequestMessageAsync(httpMethod, url, headers, content).ConfigureAwait(false))
			{
				using (var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
				{
					return await RestResponse.CreateAsync(response, _bodyContentHandler).ConfigureAwait(false);
				}
			}
		}

		public Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, object>(httpMethod, url, default, default, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, object>(httpMethod, url, default, headers, cancellationToken);
		}

		public Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(httpMethod, url, content, default, cancellationToken);
		}

		public async Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default)
		{
			using (var request = await BuildHttpRequestMessageAsync(httpMethod, url, headers, content).ConfigureAwait(false))
			{
				using (var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
				{
					return await RestResponse<TResponseContent>.CreateAsync(response, _bodyContentHandler).ConfigureAwait(false);
				}
			}
		}





		public void Dispose()
		{
			// DO NOT DISPOSE HTTP CLIENT AS HTTP CLIENT IS SINGLETON
			//_httpClient?.Dispose();
		}





		private void ValidateInstance()
		{
			var validators = ServiceProvider.GetServices<IRestClientValidator>();
			if (validators != null)
			{
				foreach (var validator in validators)
				{
					validator.Validate();
				}
			}
		}

		private async Task<HttpRequestMessage> BuildHttpRequestMessageAsync<TRequestContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers, RestRequestContent<TRequestContent> content = default)
		{
			if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

			var uri = new Uri(url.TrimStart('/'), UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				if (string.IsNullOrWhiteSpace(_options.BaseAddress?.OriginalString)) throw new Exception($"{nameof(RestClientOptions.BaseAddress)} is set on {nameof(RestClientOptions)}.");
				uri = new Uri(_options.BaseAddress, uri);
			}

			var request = new HttpRequestMessage(httpMethod, uri);

			if (headers != null)
			{
				foreach (var header in headers)
				{
					request.Headers.Remove(header.Key);
					request.Headers.Add(header.Key, header.Value);
				}
			}

			if (_options.DefaultRequestHeaders != null)
			{
				foreach (var header in _options.DefaultRequestHeaders)
				{
					if (request.Headers.Contains(header.Key)) continue;
					request.Headers.Add(header.Key, header.Value);
				}
			}

			await _restAuthenticationHandler.HandleAsync(request).ConfigureAwait(false);

			if (content != null)
			{
				request.Content = await _bodyContentHandler.GetHttpContentAsync(content).ConfigureAwait(false);
			}
			return request;
		}
	}
}
