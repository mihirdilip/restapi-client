// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.ContentSerializer
{
	public static class XmlExtensions
	{
		public static IRestClientBuilder AddXmlHttpContentSerializer(this IRestClientBuilder builder)
		{
			builder.AddHttpContentSerializer<TextXmlHttpContentSerializer>();
			builder.AddHttpContentSerializer<ApplicationXmlHttpContentSerializer>();
			return builder;
		}
	}
}
