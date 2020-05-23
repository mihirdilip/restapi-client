// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.ContentSerializer
{
	public static class JsonExtensions
	{
		/// <summary>
		/// Adds Json (application/json) content media type handling to the pipeline as a singleton implementation.
		/// This will process the request content to be serialized as Json when sending to the server
		/// and also, handle the response from the server with content type of Json.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddJsonHttpContentSerializer(this IRestClientBuilder builder)
		{
			builder.AddHttpContentSerializer<JsonHttpContentSerializer>();
			return builder;
		}
	}
}
