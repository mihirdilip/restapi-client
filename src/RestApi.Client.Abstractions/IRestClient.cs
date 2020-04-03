// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client
{
	/// <summary>
	/// The rest client interface for making any rest api requests.
	/// </summary>
	public interface IRestClient : IDisposable
	{
		/// <summary>
		/// This is for internal use only!
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		IServiceProvider ServiceProvider { get; }

		/// <summary>
		/// Sends a http GET request to the <paramref name="url"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> GetAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http GET request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> GetAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http GET request to the <paramref name="url"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http GET request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> GetAsync<TResponseContent>(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);







		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PostAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PostAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PostAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PostAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> PostAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);







		/// <summary>
		/// Sends a http PUT request to the <paramref name="url"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PutAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http PUT request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PutAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http PUT request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PutAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http PUT request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> PutAsync<TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http PUT request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http POST request to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> PutAsync<TResponseContent, TRequestContent>(string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);







		/// <summary>
		/// Sends a http DELETE request to the <paramref name="url"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> DeleteAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http DELETE request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> DeleteAsync(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http DELETE request to the <paramref name="url"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a http GET request to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> DeleteAsync<TResponseContent>(string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);







		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> SendAsync(HttpMethod httpMethod, string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and without any content unless there is any error with the request.
		/// </summary>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and without any content unless there is any error with the request.</returns>
		Task<IRestResponse> SendAsync<TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent>(HttpMethod httpMethod, string url, RestHttpHeaders headers, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a request of <paramref name="httpMethod"/> to the <paramref name="url"/> with request <paramref name="content"/> of type <see cref="TRequestContent"/> and with request <paramref name="headers"/>.
		/// Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.
		/// </summary>
		/// <typeparam name="TResponseContent">The response content type.</typeparam>
		/// <typeparam name="TRequestContent">The request content type.</typeparam>
		/// <param name="httpMethod">The <see cref="HttpMethod"/>.</param>
		/// <param name="url">The url.</param>
		/// <param name="content">The request content of type <see cref="TRequestContent"/>.</param>
		/// <param name="headers">The request <see cref="RestHttpHeaders"/>.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns response with status and content of type <see cref="TResponseContent"/> unless there is any error with the request.</returns>
		Task<IRestResponse<TResponseContent>> SendAsync<TResponseContent, TRequestContent>(HttpMethod httpMethod, string url, RestRequestContent<TRequestContent> content, RestHttpHeaders headers, CancellationToken cancellationToken = default);
	}
}