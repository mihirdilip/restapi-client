// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace RestApi.Client
{
	/// <summary>
	/// The collection of request headers to be passed with requests made by the <see cref="IRestClient"/>.
	/// </summary>
	public class RestHttpHeaders : HttpHeaders
	{
		/// <summary>
		/// Adds authentication header to the collection of headers.
		/// </summary>
		/// <param name="headerValue"></param>
		public void Add(AuthenticationHeaderValue headerValue)
		{
			Add(HeaderNames.Authorization, headerValue.ToString());
		}
	}
}
