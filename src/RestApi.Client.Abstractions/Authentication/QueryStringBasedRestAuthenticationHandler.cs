// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public abstract class QueryStringBasedRestAuthenticationHandler : IRestAuthenticationHandler
	{
		public async Task<bool> HandleAsync(HttpRequestMessage request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			var extraQueryString = await GetQueryStringAsync().ConfigureAwait(false);

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

		protected abstract Task<QueryString> GetQueryStringAsync();
	}
}
