// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// API Key Authentication object to be returned by the authentication provider.
	/// </summary>
	public class ApiKeyAuthentication
	{
		internal string Key { get; }
		internal string Value { get; }

		/// <summary>
		/// API Key Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="key">The API key name. It should match to what is expected by the server.</param>
		/// <param name="value">The API key value. This is the api key value expected by the server.</param>
		public ApiKeyAuthentication(string key, string value)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

			Key = key;
			Value = value;
		}
	}
}
