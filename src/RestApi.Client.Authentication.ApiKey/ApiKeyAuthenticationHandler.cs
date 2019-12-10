// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	internal class ApiKeyInHeaderAuthenticationHandler : HeaderBasedRestAuthenticationHandler
	{
		private readonly IApiKeyAuthenticationProvider _authenticationProvider;

		public ApiKeyInHeaderAuthenticationHandler(IApiKeyAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		protected override async Task<RestHttpHeaders> GetHeadersAsync()
		{
			var authentication = await _authenticationProvider.ProvideAsync().ConfigureAwait(false);
			return new RestHttpHeaders { {authentication.Key, authentication.Value} };
		}
	}

	internal class ApiKeyInQueryParamsAuthenticationHandler : QueryStringBasedRestAuthenticationHandler
	{
		private readonly IApiKeyAuthenticationProvider _authenticationProvider;

		public ApiKeyInQueryParamsAuthenticationHandler(IApiKeyAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		protected override async Task<QueryString> GetQueryStringAsync()
		{
			var authentication = await _authenticationProvider.ProvideAsync().ConfigureAwait(false);
			return new QueryString().Add(authentication.Key, authentication.Value);
		}
	}
}
