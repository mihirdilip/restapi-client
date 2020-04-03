// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// An abstract class which can be inherited to implement Authorization header based authentication handler.
	/// It adds the resolved authentication header to the collection of request headers to the request pipeline.
	/// </summary>
	public abstract class AuthorizationHeaderRestAuthenticationHandler : HeaderBasedRestAuthenticationHandler
	{
		/// <summary>
		/// Do not need to override this method. Only override if really necessary.
		/// Gets a collection of request headers to be added to the request pipeline.
		/// </summary>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>An instance of <see cref="RestHttpHeaders"/>.</returns>
		protected override async Task<RestHttpHeaders> GetHeadersAsync(CancellationToken cancellationToken)
		{
			var authHeaderValue = await GetAuthenticationHeaderValueAsync(cancellationToken).ConfigureAwait(false);
			return authHeaderValue == null 
				? new RestHttpHeaders() 
				: new RestHttpHeaders { authHeaderValue };
		}

		/// <summary>
		/// Gets the <see cref="AuthenticationHeaderValue"/> to be added to the request pipeline.
		/// Override this method and return an instance of <see cref="AuthenticationHeaderValue"/>.
		/// </summary>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>An instance of <see cref="AuthenticationHeaderValue"/>.</returns>
		protected abstract Task<AuthenticationHeaderValue> GetAuthenticationHeaderValueAsync(CancellationToken cancellationToken);
	}
}
