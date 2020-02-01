// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client
{
	public interface IRestClient : IDisposable
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		IServiceProvider ServiceProvider { get; }

		Task<IRestResponse> GetAsync(string url, CancellationToken cancellationToken = default);
		Task<IRestResponse> GetAsync(string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);


		Task<IRestResponse> PostAsync(string url, CancellationToken cancellationToken = default);
		Task<IRestResponse> PostAsync(string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);
		Task<IRestResponse> PostAsync<TRequestContent>(string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse> PostAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);


		Task<IRestResponse> PutAsync<TRequestContent>(string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse> PutAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);


		Task<IRestResponse> DeleteAsync(string url, CancellationToken cancellationToken = default);
		Task<IRestResponse> DeleteAsync(string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);


		Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default);
		Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers = default, CancellationToken cancellationToken = default);
		Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, string contentMediaType = default, CancellationToken cancellationToken = default);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = default, string contentMediaType = default, CancellationToken cancellationToken = default);
	}
}