// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client
{
	public class RestClientOptions 
	{
		private Uri _baseAddress;

		public Uri BaseAddress
		{
			get => _baseAddress;
			set
			{
				var uri = value?.ToString();
				if (!string.IsNullOrWhiteSpace(uri) && !uri.EndsWith("/"))
				{
					uri += "/";
					_baseAddress = new Uri(uri);
				}
				else
				{
					_baseAddress = value;
				}
			}
		}

		public RestHttpHeaders DefaultRequestHeaders { get; set; } = new RestHttpHeaders();
	}
}
