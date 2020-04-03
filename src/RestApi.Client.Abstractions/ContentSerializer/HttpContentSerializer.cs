// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	/// <summary>
	/// An abstract class which can be inherited to implement content serializer to add to the pipeline.
	/// Content serializer helps in serializing/converting the request body content into <see cref="HttpContent"/> as per the content media type.
	/// And vice-versa, deserializing/converting the <see cref="HttpContent"/> into response content type.
	/// </summary>
	public abstract class HttpContentSerializer<TSerializer> : IHttpContentSerializer
		where TSerializer : IHttpContentSerializer
	{
		/// <summary>
		/// Gets the content media type supported by this content serializer.
		/// </summary>
		public abstract string ContentMediaType { get; }

		/// <summary>
		///	Implementation for serializing/converting the request body content of type <see cref="TRequestContent"/> into <see cref="HttpContent"/> as per the content media type supported by this content serializer.
		/// Override this method and return an instance of <see cref="HttpContent"/>.
		/// </summary>
		/// <typeparam name="TRequestContent">The type if the request content.</typeparam>
		/// <param name="content">The request content to be serialized/converted into <see cref="HttpContent"/>.</param>
		/// <returns>Returns an instance of <see cref="HttpContent"/> serialize/converted from <paramref name="content"/>.</returns>
		protected abstract Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content);

		/// <summary>
		/// Implementation for serializing/converting the response body content of type <see cref="HttpContent"/> into <see cref="TResponseContent"/> as per the content media type supported by this content serializer.
		/// Override this method and return an instance of <see cref="TResponseContent"/>.
		/// </summary>
		/// <typeparam name="TResponseContent">The type of response content to be return.</typeparam>
		/// <param name="content">The response <see cref="HttpContent"/> to be serialized/converted into <see cref="TResponseContent"/>.</param>
		/// <returns>Returns and instance of <see cref="TResponseContent"/> serialize/converted from <paramref name="content"/>.</returns>
		protected abstract Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content);




		/// <summary>
		/// Do not need to override this method. Only override if really necessary.
		/// Gets an instance of <see cref="HttpContent"/> by serializing/converting the request body content of type <see cref="TRequestContent"/> as per the content media type supported by this content serializer.
		/// </summary>
		/// <typeparam name="TRequestContent">The type if the request content.</typeparam>
		/// <param name="content">The request content to be serialized/converted into <see cref="HttpContent"/>.</param>
		/// <returns>Returns an instance of <see cref="HttpContent"/> serialize/converted from <paramref name="content"/>.</returns>
		public Task<HttpContent> GetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			if (content == null) throw new ArgumentNullException(nameof(content));
			return ProtectedGetHttpContentAsync(content);
		}

		/// <summary>
		/// Do not need to override this method. Only override if really necessary.
		/// Gets an instance of <see cref="TResponseContent"/> by serializing/converting the response body content of type <see cref="HttpContent"/> as per the content media type supported by this content serializer.
		/// </summary>
		/// <typeparam name="TResponseContent">The type of response content to be return.</typeparam>
		/// <param name="content">The response <see cref="HttpContent"/> to be serialized/converted into <see cref="TResponseContent"/>.</param>
		/// <returns>Returns and instance of <see cref="TResponseContent"/> serialize/converted from <paramref name="content"/>.</returns>
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