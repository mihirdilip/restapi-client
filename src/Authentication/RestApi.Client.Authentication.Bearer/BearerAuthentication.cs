﻿// Copyright (c) Mihir Dilip. All rights reserved.
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

		public BearerAuthentication(string scheme, string accessToken)
		{
			if (string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

			Scheme = scheme;
			Value = accessToken;
		}
	}
}