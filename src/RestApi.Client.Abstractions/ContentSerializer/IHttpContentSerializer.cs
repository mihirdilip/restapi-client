// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	/// <summary>
	/// An interface to be implemented for adding content serializer to the pipeline.
	/// Content serializer helps in serializing/converting the request body content into <see cref="HttpContent"/> as per the content media type.
	/// And vice-versa, deserializing/converting the <see cref="HttpContent"/> into response content type.
	/// </summary>
	public interface IHttpContentSerializer
	{
		/// <summary>
		/// Gets the content media type supported by this content serializer.
		/// </summary>
		string ContentMediaType { get; }

		/// <summary>
		/// Gets an instance of <see cref="HttpContent"/> by serializing/converting the request body content of type <see cref="TRequestContent"/> as per the content media type supported by this content serializer.
		/// </summary>
		/// <typeparam name="TRequestContent">The type if the request content.</typeparam>
		/// <param name="content">The request content to be serialized/converted into <see cref="HttpContent"/>.</param>
		/// <returns>Returns an instance of <see cref="HttpContent"/> serialize/converted from <paramref name="content"/>.</returns>
		Task<HttpContent> GetHttpContentAsync<TRequestContent>(TRequestContent content);

		/// <summary>
		/// Gets an instance of <see cref="TResponseContent"/> by serializing/converting the response body content of type <see cref="HttpContent"/> as per the content media type supported by this content serializer.
		/// </summary>
		/// <typeparam name="TResponseContent">The type of response content to be return.</typeparam>
		/// <param name="content">The response <see cref="HttpContent"/> to be serialized/converted into <see cref="TResponseContent"/>.</param>
		/// <returns>Returns and instance of <see cref="TResponseContent"/> serialize/converted from <paramref name="content"/>.</returns>
		Task<TResponseContent> GetResponseContentAsync<TResponseContent>(HttpContent content);
	}
}