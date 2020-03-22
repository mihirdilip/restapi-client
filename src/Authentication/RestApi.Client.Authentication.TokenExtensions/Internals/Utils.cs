// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http.Headers;
using System.Text;

namespace RestApi.Client.Authentication
{
	static class Utils
	{
		internal static AuthenticationHeaderValue GetBasicAuthenticationOAuth2SpecHeaderValue(string username, string password)
		{
			return GetBasicAuthenticationHeaderValue(UrlEncoded(username), UrlEncoded(password));
        }

		internal static AuthenticationHeaderValue GetBasicAuthenticationHeaderValue(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
			if (password == null) password = string.Empty;

			return new AuthenticationHeaderValue(
				Constants.BasicScheme,
				Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))
			);
		}

        private static string UrlEncoded(string value)
		{
			return string.IsNullOrEmpty(value) 
				? string.Empty 
				: Uri.EscapeDataString(value).Replace("%20", "+");
		}
    }
}
