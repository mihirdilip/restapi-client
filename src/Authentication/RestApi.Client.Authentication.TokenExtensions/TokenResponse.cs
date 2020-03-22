// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Newtonsoft.Json;
using System;

namespace RestApi.Client.Authentication
{
    /// <summary>
    /// The response received for token request.
    /// </summary>
    public class TokenResponse 
    {
	    private const int TokenExpiryToleranceInSeconds = 60;
        private DateTime _creationTime = DateTime.Now.ToUniversalTime();

        /// <summary>
        /// Gets the access token.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.AccessToken)]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Gets the identity token.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.IdentityToken)]
        public string IdentityToken { get; internal set; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.TokenType)]
        public string TokenType { get; internal set; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.RefreshToken)]
        public string RefreshToken { get; internal set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.Error)]
        public string Error { get; internal set; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.ErrorDescription)]
        public string ErrorDescription { get; internal set; }

        /// <summary>
        /// Gets the error uri.
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.ErrorUri)]
        public string ErrorUri { get; internal set; }

        /// <summary>
        /// Gets the expires in (seconds).
        /// </summary>
        [JsonProperty(OidcConstants.TokenResponse.ExpiresIn)]
        public int ExpiresIn { get; internal set; }

        /// <summary>
        /// Gets the access token expiry in UTC datetime.
        /// </summary>
        public DateTime AccessTokenExpiry => _creationTime.AddSeconds(ExpiresIn - TokenExpiryToleranceInSeconds); 

        /// <summary>
        /// Gets the raw response (if present).
        /// </summary>
        public string RawResponse { get; internal set; }

        /// <summary>
        /// Gets the exception (if present).
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// Checks if any error exists.
        /// </summary>
        public bool HasError => !string.IsNullOrWhiteSpace(Error);

        internal bool HasExpired => AccessTokenExpiry <= DateTime.Now.ToUniversalTime();

        internal bool HasInvalidTokenError => !string.IsNullOrWhiteSpace(Error)
                                              && (
	                                              Error.Equals(OidcConstants.TokenErrors.InvalidGrant, StringComparison.OrdinalIgnoreCase)
                                                  || Error.Equals(OidcConstants.TokenErrors.ExpiredToken, StringComparison.OrdinalIgnoreCase)
	                                              || Error.Equals(OidcConstants.TokenErrors.AccessDenied, StringComparison.OrdinalIgnoreCase)
                                              );
    }
}
