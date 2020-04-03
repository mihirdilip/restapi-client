// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// An abstract class which can be inherited to implement Query String based authentication handler.
	/// It adds the resolved query string to the request pipeline.
	/// </summary>
	public abstract class QueryStringBasedRestAuthenticationHandler : IRestAuthenticationHandler
	{
		/// <summary>
		/// Do not need to override this method. Only override if really necessary.
		/// Handles the authentication by adding query string to the url of the request.
		/// </summary>
		/// <param name="request">The <see cref="HttpRequestMessage"/> to which the query string will be added.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Return a boolean indicating that it is handled.</returns>
		public async Task<bool> HandleAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (request == null) throw new ArgumentNullException(nameof(request));

			var extraQueryString = await GetQueryStringAsync(cancellationToken).ConfigureAwait(false);

			var newQueryString = new QueryString();
			if (!string.IsNullOrWhiteSpace(request.RequestUri.Query))
			{
				newQueryString = newQueryString.Add(QueryString.FromUriComponent(request.RequestUri.Query));
			}

			if (extraQueryString.HasValue)
			{
				newQueryString = newQueryString.Add(extraQueryString);
			}

			var queryBuilder = new UriBuilder(request.RequestUri)
			{
				Query = newQueryString.ToUriComponent()
			};

			request.RequestUri = queryBuilder.Uri;

			return true;
		}

		/// <summary>
		/// Gets the <see cref="QueryString"/> to be added to the request url.
		/// Override this method and return an instance of <see cref="QueryString"/>.
		/// </summary>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Returns an request query string (<see cref="QueryString"/>) to be added to the request url.</returns>
		protected abstract Task<QueryString> GetQueryStringAsync(CancellationToken cancellationToken);
	}
}
