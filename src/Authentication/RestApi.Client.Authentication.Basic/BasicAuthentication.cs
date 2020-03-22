// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Text;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// Basic Authentication object to be returned by the authentication provider.
	/// </summary>
	public class BasicAuthentication
	{
		internal string Scheme { get; private set; }
		internal string Value { get; }

		/// <summary>
		/// Basic Authentication object to be returned by the authentication provider. Calculates the hash from username and password.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		public BasicAuthentication(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
			if (password == null) password = string.Empty;

			Scheme = BasicDefaults.AuthenticationScheme;
			Value = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
		}

		/// <summary>
		/// Basic Authentication object to be returned by the authentication provider.
		/// </summary>
		/// <param name="tokenHash">The token hash which will be sent as it is with Basic scheme in Authorization request header.</param>
		public BasicAuthentication(string tokenHash)
		{
			if (string.IsNullOrWhiteSpace(tokenHash)) throw new ArgumentNullException(nameof(tokenHash));

			Scheme = BasicDefaults.AuthenticationScheme;
			Value = tokenHash;
		}

		/// <summary>
		/// Overrides the scheme name passed to the Authorization request header. Default scheme is Basic.
		/// </summary>
		/// <param name="overriddenScheme">The custom scheme name to be used for the authorization header.</param>
		public void OverrideScheme(string overriddenScheme)
		{
			Scheme = overriddenScheme;
		}
	}
}
