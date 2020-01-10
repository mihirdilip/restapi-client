// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public abstract class AuthorizationHeaderRestAuthenticationHandler : HeaderBasedRestAuthenticationHandler
	{
		protected override async Task<RestHttpHeaders> GetHeadersAsync()
		{
			var authHeaderValue = await GetAuthenticationHeaderValueAsync().ConfigureAwait(false);
			return authHeaderValue == null 
				? new RestHttpHeaders() 
				: new RestHttpHeaders { authHeaderValue };
		}

		protected abstract Task<AuthenticationHeaderValue> GetAuthenticationHeaderValueAsync();
	}
}
