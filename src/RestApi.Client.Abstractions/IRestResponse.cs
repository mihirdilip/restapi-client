// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace RestApi.Client
{
	public interface IRestResponse : IDisposable
	{
		HttpStatusCode StatusCode { get; }
		string ReasonPhrase { get; }
		Version Version { get; }
		bool IsSuccessStatusCode { get; }
		HttpResponseHeaders Headers { get; }
		HttpContentHeaders ContentHeaders { get; }
		ValidationProblemDetails ProblemDetails { get; }
	}

	public interface IRestResponse<out TResponseContent> : IRestResponse
	{
		TResponseContent Content { get; }
	}
}
