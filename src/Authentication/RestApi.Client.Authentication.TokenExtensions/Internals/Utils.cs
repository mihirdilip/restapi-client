// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http.Headers;
using System.Text;

namespace RestApi.Client.Authentication
{
	static class Utils
	{
		internal static AuthenticationHeaderValue GetBasicAuthenticationOAuth2SpecHeaderValue(string clientId, string clientSecret)
		{
			if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
			if (clientSecret == null) clientSecret = "";

            return new AuthenticationHeaderValue(
	            Constants.BasicScheme, 
	            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{UrlEncoded(clientId)}:{UrlEncoded(clientSecret)}"))
	        );
        }

		internal static AuthenticationHeaderValue GetBasicAuthenticationHeaderValue(string clientId, string clientSecret)
		{
			if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
			if (clientSecret == null) clientSecret = "";

			return new AuthenticationHeaderValue(
				Constants.BasicScheme,
				Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"))
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
