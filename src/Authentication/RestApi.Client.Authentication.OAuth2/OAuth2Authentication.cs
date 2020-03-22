// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// OAuth 2.0 Authentication object to be returned by the authentication provider.
	/// </summary>
	public class OAuth2Authentication
	{
		internal string Scheme { get; }
		internal string Value { get; }

		/// <summary>
		/// OAuth 2.0 Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		public OAuth2Authentication(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = OAuth2Defaults.AuthenticationScheme;
			Value = accessToken;
		}

		/// <summary>
		/// OAuth 2.0 Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="scheme">The custom scheme name to be passed to the Authorization header. Default scheme is Bearer.</param>
		/// <param name="accessToken">The access token.</param>
		public OAuth2Authentication(string scheme,  string accessToken)
		{
			if (string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = scheme;
			Value = accessToken;
		}
	}
}
