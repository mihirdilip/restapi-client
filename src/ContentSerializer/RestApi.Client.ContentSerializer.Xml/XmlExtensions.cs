// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.ContentSerializer
{
	public static class XmlExtensions
	{
		/// <summary>
		/// Adds Xml (text/xml and application/xml) content media type handling to the pipeline as a singleton implementation.
		/// This will process the request content to be serialized as Xml when sending to the server
		/// and also, handle the response from the server with content type of Xml.
		/// </summary>
		/// <param name="builder">The rest client builder.</param>
		/// <returns>The rest client builder.</returns>
		public static IRestClientBuilder AddXmlHttpContentSerializer(this IRestClientBuilder builder)
		{
			builder.AddHttpContentSerializer<TextXmlHttpContentSerializer>();
			builder.AddHttpContentSerializer<ApplicationXmlHttpContentSerializer>();
			return builder;
		}
	}
}
