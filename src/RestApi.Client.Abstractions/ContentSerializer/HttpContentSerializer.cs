// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	public abstract class HttpContentSerializer<TSerializer> : IHttpContentSerializer
		where TSerializer : IHttpContentSerializer
	{
		public abstract string ContentMediaType { get; }
		protected abstract Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content);
		protected abstract Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content);

		public Task<HttpContent> GetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			if (content == null) throw new ArgumentNullException(nameof(content));
			return ProtectedGetHttpContentAsync(content);
		}

		public Task<TResponseContent> GetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			if (content == null) throw new ArgumentNullException(nameof(content));

			var mediaType = content.Headers?.ContentType?.MediaType;

			if (string.IsNullOrWhiteSpace(mediaType))
			{
				throw new InvalidOperationException($"Empty HttpContent media type cannot be handled.");
			}

			if (!ContentMediaType.Equals(mediaType, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException($"HttpContent with invalid media type '{mediaType}' passed to '{typeof(TSerializer).Name}' which supports '{ContentMediaType}'.");
			}

			return ProtectedGetResponseContentAsync<TResponseContent>(content);
		}
	}
}