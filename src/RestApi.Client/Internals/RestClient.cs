// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Extensions.Options;
using RestApi.Client.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client
{
	internal class RestClient : IRestClient
	{
		private readonly RestClientOptions _options;
		private readonly HttpClient _httpClient;
		private readonly IHttpContentHandler _bodyContentHandler;
		private readonly IRestAuthenticationHandler _restAuthenticationHandler;

		public RestClient(IOptions<RestClientOptions> options, IHttpClientFactory httpClientFactory, IHttpContentHandler bodyContentHandler, IRestAuthenticationHandler restAuthenticationHandler)
		{
			_options = options.Value;
			_httpClient = httpClientFactory.CreateClient();
			_bodyContentHandler = bodyContentHandler;
			_restAuthenticationHandler = restAuthenticationHandler;
		}

		public Task<IRestResponse> GetAsync(string url, RestHttpHeaders headers = null)
		{
			return SendAsync(HttpMethod.Get, url, headers);
		}

		public Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, RestHttpHeaders headers = null)
		{
			return SendAsync<TResponseContent>(HttpMethod.Get, url, headers);
		}

		public Task<IRestResponse> PostAsync(string url, RestHttpHeaders headers = null)
		{
			return SendAsync(HttpMethod.Post, url, headers);
		}

		public Task<IRestResponse> PostAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			return SendAsync(HttpMethod.Post, url, content, headers, contentMediaType);
		}

		public Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Post, url, content, headers, contentMediaType);
		}

		public Task<IRestResponse> PutAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			return SendAsync(HttpMethod.Put, url, content, headers, contentMediaType);
		}

		public Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			return SendAsync<TResponseContent, TRequestContent>(HttpMethod.Put, url, content, headers, contentMediaType);
		}

		public Task<IRestResponse> DeleteAsync(string url, RestHttpHeaders headers = null)
		{
			return SendAsync(HttpMethod.Delete, url, headers);
		}

		public Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, RestHttpHeaders headers = null)
		{
			return SendAsync<TResponseContent>(HttpMethod.Delete, url, headers);
		}

		public Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, RestHttpHeaders headers = null)
		{
			return SendAsync<object>(httpMethod, url, null, headers, null);
		}

		public Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers = null)
		{
			return SendAsync<TResponseContent, object>(httpMethod, url, null, headers, null);
		}

		public async Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			using (var request = await BuildHttpRequestMessageAsync(httpMethod, url, headers, content, contentMediaType).ConfigureAwait(false))
			{
				using (var response = await _httpClient.SendAsync(request).ConfigureAwait(false))
				{
					return await RestResponse.CreateAsync(response, _bodyContentHandler).ConfigureAwait(false);
				}
			}
			
		}

		public async Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default)
		{
			using (var request = await BuildHttpRequestMessageAsync(httpMethod, url, headers, content, contentMediaType).ConfigureAwait(false))
			{
				using (var response = await _httpClient.SendAsync(request).ConfigureAwait(false))
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

		private async Task<HttpRequestMessage> BuildHttpRequestMessageAsync<TRequestContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers, TRequestContent body = default, string contentMediaType = default)
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

			if (body != null)
			{
				request.Content = await _bodyContentHandler.GetHttpContentAsync(contentMediaType, body).ConfigureAwait(false);
			}
			return request;
		}
	}
}
