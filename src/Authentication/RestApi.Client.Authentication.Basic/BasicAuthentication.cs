// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Text;

namespace RestApi.Client.Authentication
{
	public class BasicAuthentication
	{
		internal string Scheme { get; private set; }
		internal string Value { get; }

		public BasicAuthentication(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

			Scheme = BasicDefaults.AuthenticationScheme;
			Value = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"));
		}

		public BasicAuthentication(string token)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

			Scheme = BasicDefaults.AuthenticationScheme;
			Value = token;
		}

		/// <summary>
		/// Overrides the scheme name passed to the Authorization header. Default scheme name is Basic.
		/// </summary>
		/// <param name="overriddenScheme"></param>
		public void OverrideScheme(string overriddenScheme)
		{
			Scheme = overriddenScheme;
		}
	}
}
