// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestApi.Client
{
	internal class RestResponse : IRestResponse
	{
		internal static async Task<RestResponse> CreateAsync(HttpResponseMessage response, IHttpContentHandler bodyContentHandler)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));
			if (bodyContentHandler == null) throw new ArgumentNullException(nameof(bodyContentHandler));

			var apiResponse = new RestResponse
			{
				StatusCode = response.StatusCode,
				Headers = response.Headers,
				IsSuccessStatusCode = response.IsSuccessStatusCode,
				ReasonPhrase = response.ReasonPhrase,
				Version = response.Version,
				ProblemDetails = !response.IsSuccessStatusCode ? await bodyContentHandler.GetResponseContentAsync<ValidationProblemDetails>(response.Content).ConfigureAwait(false) : null
			};

			return apiResponse;
		}

		internal RestResponse()
		{
		}

		public HttpStatusCode StatusCode { get; protected set; }
		public string ReasonPhrase { get; protected set; }
		public Version Version { get; protected set; }
		public bool IsSuccessStatusCode { get; protected set; }
		public HttpResponseHeaders Headers { get; protected set; }
		public ValidationProblemDetails ProblemDetails { get; protected set; }

		public virtual void Dispose()
		{
		}
	}

	internal class RestResponse<TResponseContent> : RestResponse, IRestResponse<TResponseContent>
	{
		internal new static async Task<RestResponse<TResponseContent>> CreateAsync(HttpResponseMessage response, IHttpContentHandler bodyContentHandler)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));
			if (bodyContentHandler == null) throw new ArgumentNullException(nameof(bodyContentHandler));

			var apiResponse = new RestResponse<TResponseContent>
			{
				StatusCode = response.StatusCode,
				Headers = response.Headers,
				ContentHeaders = response.Content?.Headers,
				IsSuccessStatusCode = response.IsSuccessStatusCode,
				ReasonPhrase = response.ReasonPhrase,
				Version = response.Version,
				Content = response.IsSuccessStatusCode ? await bodyContentHandler.GetResponseContentAsync<TResponseContent>(response.Content).ConfigureAwait(false) : default,
				ProblemDetails = !response.IsSuccessStatusCode ? await bodyContentHandler.GetResponseContentAsync<ValidationProblemDetails>(response.Content).ConfigureAwait(false) : default
			};

			return apiResponse;
		}

		private RestResponse()
		{
		}

		public TResponseContent Content { get; private set; }
		public HttpContentHeaders ContentHeaders { get; protected set; }

		public override void Dispose()
		{
			(Content as IDisposable)?.Dispose();
			base.Dispose();
		}
	}
}
