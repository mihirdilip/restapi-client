// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.ContentSerializer
{
	public static class JsonExtensions
	{
		public static IRestClientBuilder AddJsonHttpContentSerializer(this IRestClientBuilder builder)
		{
			builder.AddHttpContentSerializer<JsonHttpContentSerializer>();
			return builder;
		}
	}
}
