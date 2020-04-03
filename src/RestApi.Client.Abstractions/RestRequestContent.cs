// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client
{
	/// <summary>
	/// Request content to be passed with requests made by the <see cref="IRestClient"/>.
	/// </summary>
	/// <typeparam name="TContent">The type of the content object.</typeparam>
	public class RestRequestContent<TContent> 
	{
		/// <summary>
		/// Creates an instance of <see cref="RestRequestContent{TContent}"/>.
		/// </summary>
		/// <param name="content">The request content.</param>
		/// <param name="contentMediaType">The media mime type (<see cref="MediaMimeTypes"/>) of content to be used when serializing the <paramref name="content"/>.</param>
		public RestRequestContent(TContent content, string contentMediaType)
		{
			Content = content;
			ContentMediaType = contentMediaType;
		}

		/// <summary>
		/// Gets the request content.
		/// </summary>
		public TContent Content { get; }

		/// <summary>
		/// Gets the media mime type <see cref="MediaMimeTypes"/> of content to be used when serializing the content.
		/// </summary>
		public string ContentMediaType { get; }
	}
}
