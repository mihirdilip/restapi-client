// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client.Authentication
{
	public class BearerAuthentication
	{
		internal string Scheme { get; private set; }
		internal string Value { get; }

		public BearerAuthentication(string accessToken)
		{
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = BearerDefaults.AuthenticationScheme;
			Value = accessToken;
		}

		/// <summary>
		/// Overrides the scheme name passed to the Authorization header. Default scheme name is Bearer.
		/// </summary>
		/// <param name="overriddenScheme"></param>
		public void OverrideScheme(string overriddenScheme)
		{
			Scheme = overriddenScheme;
		}
	}
}
