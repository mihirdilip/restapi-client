// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using RestApi.Client.ContentSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client
{
	internal interface IHttpContentHandler
	{
		Task<HttpContent> GetHttpContentAsync<TRequestContent>(RestRequestContent<TRequestContent> content);
		Task<TResponseContent> GetResponseContentAsync<TResponseContent>(HttpContent content);
	}

	internal class HttpContentHandler : IHttpContentHandler
	{
		private readonly IEnumerable<IHttpContentSerializer> _serializers;

		public HttpContentHandler(IEnumerable<IHttpContentSerializer> serializers)
		{
			_serializers = serializers;
		}

		private IHttpContentSerializer GetAndCheckSerializer(string contentMediaType)
		{
			if (string.IsNullOrWhiteSpace(contentMediaType)) throw new ArgumentNullException(nameof(contentMediaType));
			var serializer = _serializers.FirstOrDefault(s => s.ContentMediaType.Equals(contentMediaType, StringComparison.OrdinalIgnoreCase));
			if (serializer == null) throw new Exception($"Serializer not found for content media type '{contentMediaType}'.");
			return serializer;
		}

		public async Task<HttpContent> GetHttpContentAsync<TRequestContent>(RestRequestContent<TRequestContent> content)
		{
			if (content == default || string.IsNullOrWhiteSpace(content.ContentMediaType)) return default;
			return await GetAndCheckSerializer(content.ContentMediaType).GetHttpContentAsync(content).ConfigureAwait(false);
		}

		public async Task<TResponseContent> GetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			var contentMediaType = content?.Headers?.ContentType?.MediaType;
			if (string.IsNullOrWhiteSpace(contentMediaType)) return default; 
			return await GetAndCheckSerializer(contentMediaType).GetResponseContentAsync<TResponseContent>(content).ConfigureAwait(false);
		}
	}
}