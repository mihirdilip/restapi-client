// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace RestApi.Client
{
	/// <summary>
	/// An interface of rest response which can be implemented and is returned as response without any body content to the request by <see cref="IRestClient"/>.
	/// </summary>
	public interface IRestResponse : IDisposable
	{
		/// <summary>
		///	Get the response status code.
		/// </summary>
		HttpStatusCode StatusCode { get; }

		/// <summary>
		/// Gets the response reason phrase.
		/// </summary>
		string ReasonPhrase { get; }

		/// <summary>
		/// Gets the version.
		/// </summary>
		Version Version { get; }

		/// <summary>
		/// Gets response success check.
		/// </summary>
		bool IsSuccessStatusCode { get; }

		/// <summary>
		/// Gets response headers.
		/// </summary>
		HttpResponseHeaders Headers { get; }

		/// <summary>
		/// Get the problem details. It contains any errors returned in response.
		/// </summary>
		ValidationProblemDetails ProblemDetails { get; }
	}

	/// <summary>
	/// An interface of rest response which can be implemented and is returned as response with a body content of type <see cref="TResponseContent"/> to the request by <see cref="IRestClient"/>.
	/// </summary>
	/// <typeparam name="TResponseContent">The type of the response content.</typeparam>
	public interface IRestResponse<out TResponseContent> : IRestResponse
	{
		/// <summary>
		/// Gets the response content.
		/// </summary>
		TResponseContent Content { get; }

		/// <summary>
		/// Gets response content headers.
		/// </summary>
		HttpContentHeaders ContentHeaders { get; }
	}
}
