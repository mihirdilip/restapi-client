// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// Bearer Authentication object to be returned by the authentication provider.
	/// </summary>
	public class BearerAuthentication
	{
		internal string Scheme { get; }
		internal string Value { get; }

		/// <summary>
		/// Bearer Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		public BearerAuthentication(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = BearerDefaults.AuthenticationScheme;
			Value = accessToken;
		}

		/// <summary>
		/// Bearer Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="scheme">The custom scheme name to be passed to the Authorization header. Default scheme is Bearer.</param>
		/// <param name="accessToken">The access token.</param>
		public BearerAuthentication(string scheme, string accessToken)
		{
			if (string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = scheme;
			Value = accessToken;
		}
	}
}
