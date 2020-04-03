// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// An abstract class which can be inherited to implement header based authentication handler.
	/// It adds the resolved headers to the request pipeline.
	/// </summary>
	public abstract class HeaderBasedRestAuthenticationHandler : IRestAuthenticationHandler
	{
		/// <summary>
		/// Do not need to override this method. Only override if really necessary.
		/// Handles the authentication by adding headers to the request pipeline.
		/// </summary>
		/// <param name="request">The <see cref="HttpRequestMessage"/> to which the headers will be added.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Return a boolean indicating that it is handled.</returns>
		public async Task<bool> HandleAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			if (request == null) throw new ArgumentNullException(nameof(request));

			var headers = await GetHeadersAsync(cancellationToken).ConfigureAwait(false);
			if (headers == null) return true;

			foreach (var header in headers)
			{
				request.Headers.Remove(header.Key);
				request.Headers.Add(header.Key, header.Value);
			}
			
			return true;
		}

		/// <summary>
		/// Gets the <see cref="RestHttpHeaders"/> to be added to the request pipeline.
		/// Override this method and return an instance of <see cref="RestHttpHeaders"/>.
		/// </summary>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns an request headers (<see cref="RestHttpHeaders"/>) to be added to the request pipeline.</returns>
		protected abstract Task<RestHttpHeaders> GetHeadersAsync(CancellationToken cancellationToken);
	}
}
