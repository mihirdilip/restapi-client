// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// Specifies the client authentication method used when authenticating with the authorization server.
	/// </summary>
	public enum ClientAuthenticationMethod
	{
		/// <summary>
		/// Authorization header oauth 2.0 spec basic authentication. Uses the encoding as described in the OAuth 2.0 spec (https://tools.ietf.org/html/rfc6749#section-2.3.1). Base64(urlformencode(client_id) + ":" + urlformencode(client_secret)).
		/// </summary>
		HeaderBasicAuthenticationOAuth2Spec,

		/// <summary>
		/// Authorization header basic authentication. Uses the encoding as described in the original basic authentication spec (https://tools.ietf.org/html/rfc2617#section-2 - used by some non-OAuth 2.0 compliant authorization servers). Base64(client_id + ":" + client_secret).
		/// </summary>
		HeaderBasicAuthentication,

		/// <summary>
		/// Post values of client_id and client_secret in body of the request.
		/// </summary>
		Post
	}
}