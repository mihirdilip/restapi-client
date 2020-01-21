// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client
{
	public interface IRestClient : IDisposable
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		IServiceProvider ServiceProvider { get; }

		Task<IRestResponse> GetAsync(string url, RestHttpHeaders headers = null);
		Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, RestHttpHeaders headers = null);
		

		Task<IRestResponse> PostAsync(string url, RestHttpHeaders headers = null);
		Task<IRestResponse> PostAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);
		Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);
		

		Task<IRestResponse> PutAsync<TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);
		Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);


		Task<IRestResponse> DeleteAsync(string url, RestHttpHeaders headers = null);
		Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, RestHttpHeaders headers = null);


		Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, RestHttpHeaders headers = null);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers = null);
		Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, TRequestContent content, RestHttpHeaders headers = null, string contentMediaType = default);
	}
}