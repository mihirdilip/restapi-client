// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.ContentSerializer
{
	public static class PlainTextExtensions
	{
		/// <summary>
		/// Adds Plain Text (text/plain) content media type handling to the pipeline as a singleton implementation.
		/// This will process the request content to be serialized as Plain Text when sending to the server
		/// and, also handle the response from the server with content type of Plain Text.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddPlainTextHttpContentSerializer(this IRestClientBuilder builder)
		{
			builder.AddHttpContentSerializer<PlainTextHttpContentSerializer>();
			return builder;
		}
	}
}
