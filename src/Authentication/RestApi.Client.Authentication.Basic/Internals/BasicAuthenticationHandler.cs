// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	internal class BasicAuthenticationHandler : AuthorizationHeaderRestAuthenticationHandler
	{
		private readonly IBasicAuthenticationProvider _authenticationProvider;

		public BasicAuthenticationHandler(IBasicAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		protected override async Task<AuthenticationHeaderValue> GetAuthenticationHeaderValueAsync(CancellationToken cancellationToken)
		{
			var basicAuth = await _authenticationProvider.ProvideAsync(cancellationToken).ConfigureAwait(false);
			return basicAuth == null 
				? null 
				: new AuthenticationHeaderValue(basicAuth.Scheme, basicAuth.Value);
		}
	}
}
