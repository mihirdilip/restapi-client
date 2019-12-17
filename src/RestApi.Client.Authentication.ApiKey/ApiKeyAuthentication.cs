// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	public class ApiKeyAuthentication
	{
		public ApiKeyAuthentication(string key, string value)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

			Key = key;
			Value = value;
		}

		public string Key { get; }
		public string Value { get; }
	}
}
