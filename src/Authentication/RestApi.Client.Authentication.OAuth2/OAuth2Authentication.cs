// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	public class OAuth2Authentication
	{
		internal string Scheme { get; private set; }
		internal string Value { get; }

		public OAuth2Authentication(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = OAuth2Defaults.AuthenticationScheme;
			Value = accessToken;
		}

		public OAuth2Authentication(string scheme,  string accessToken)
		{
			if (string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = scheme;
			Value = accessToken;
		}
	}
}
