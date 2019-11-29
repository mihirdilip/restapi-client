// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace RestApi.Client
{
	public class RestHttpHeaders : HttpHeaders
	{
		public void Add(AuthenticationHeaderValue headerValue)
		{
			Add(HeaderNames.Authorization, headerValue.ToString());
		}
	}
}
