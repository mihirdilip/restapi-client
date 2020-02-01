// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public abstract class HeaderBasedRestAuthenticationHandler : IRestAuthenticationHandler
	{
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

		protected abstract Task<RestHttpHeaders> GetHeadersAsync(CancellationToken cancellationToken);
	}
}
