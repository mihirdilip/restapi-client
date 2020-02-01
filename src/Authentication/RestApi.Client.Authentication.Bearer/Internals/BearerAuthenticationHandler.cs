// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	internal class BearerAuthenticationHandler : AuthorizationHeaderRestAuthenticationHandler
	{
		private readonly IBearerAuthenticationProvider _authenticationProvider;

		public BearerAuthenticationHandler(IBearerAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		protected override async Task<AuthenticationHeaderValue> GetAuthenticationHeaderValueAsync(CancellationToken cancellationToken)
		{
			var authentication = await _authenticationProvider.ProvideAsync(cancellationToken).ConfigureAwait(false);
			return authentication == null
				? null 
				: new AuthenticationHeaderValue(authentication.Scheme, authentication.Value);
		}
	}
}
