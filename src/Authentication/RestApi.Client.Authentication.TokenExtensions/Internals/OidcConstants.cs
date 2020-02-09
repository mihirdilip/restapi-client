namespace RestApi.Client.Authentication
{
	internal static class OidcConstants
	{
		public static class TokenRequest
		{
			public const string GrantType = "grant_type";
			public const string ClientId = "client_id";
			public const string ClientSecret = "client_secret";
			public const string ClientAssertion = "client_assertion";
			public const string ClientAssertionType = "client_assertion_type";
			public const string Assertion = "assertion";
			public const string RefreshToken = "refresh_token";
			public const string Scope = "scope";
			public const string UserName = "username";
			public const string Password = "password";
		}

		public static class TokenErrors
		{
			public const string InvalidRequest = "invalid_request";
			public const string InvalidClient = "invalid_client";
			public const string InvalidGrant = "invalid_grant";
			public const string UnauthorizedClient = "unauthorized_client";
			public const string UnsupportedGrantType = "unsupported_grant_type";
			public const string UnsupportedResponseType = "unsupported_response_type";
			public const string InvalidScope = "invalid_scope";
			public const string AuthorizationPending = "authorization_pending";
			public const string AccessDenied = "access_denied";
			public const string SlowDown = "slow_down";
			public const string ExpiredToken = "expired_token";
			public const string InvalidTarget = "invalid_target";
		}

		public static class TokenResponse
		{
			public const string AccessToken = "access_token";
			public const string ExpiresIn = "expires_in";
			public const string TokenType = "token_type";
			public const string RefreshToken = "refresh_token";
			public const string IdentityToken = "id_token";
			public const string Error = "error";
			public const string ErrorDescription = "error_description";
			public const string ErrorUri = "error_uri";
		}

		public static class TokenIntrospectionRequest
		{
			public const string Token = "token";
			public const string TokenTypeHint = "token_type_hint";
		}

		public static class TokenTypes
		{
			public const string AccessToken = "access_token";
			public const string IdentityToken = "id_token";
			public const string RefreshToken = "refresh_token";
		}

		public static class TokenTypeIdentifiers
		{
			public const string AccessToken = "urn:ietf:params:oauth:token-type:access_token";
			public const string IdentityToken = "urn:ietf:params:oauth:token-type:id_token";
			public const string RefreshToken = "urn:ietf:params:oauth:token-type:refresh_token";
			public const string Saml11 = "urn:ietf:params:oauth:token-type:saml1";
			public const string Saml2 = "urn:ietf:params:oauth:token-type:saml2";
			public const string Jwt = "urn:ietf:params:oauth:token-type:jwt";
		}

		public static class AuthenticationSchemes
		{
			public const string AuthorizationHeaderBearer = "Bearer";
			public const string FormPostBearer = "access_token";
			public const string QueryStringBearer = "access_token";

			public const string AuthorizationHeaderPop = "PoP";
			public const string FormPostPop = "pop_access_token";
			public const string QueryStringPop = "pop_access_token";
		}

		public static class GrantTypes
		{
			public const string Password = "password";
			public const string AuthorizationCode = "authorization_code";
			public const string ClientCredentials = "client_credentials";
			public const string RefreshToken = "refresh_token";
			public const string Implicit = "implicit";
			public const string Saml2Bearer = "urn:ietf:params:oauth:grant-type:saml2-bearer";
			public const string JwtBearer = "urn:ietf:params:oauth:grant-type:jwt-bearer";
			public const string DeviceCode = "urn:ietf:params:oauth:grant-type:device_code";
			public const string TokenExchange = "urn:ietf:params:oauth:grant-type:token-exchange";
		}

		public static class ClientAssertionTypes
		{
			public const string JwtBearer = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
			public const string SamlBearer = "urn:ietf:params:oauth:client-assertion-type:saml2-bearer";
		}

		public static class CodeChallengeMethods
		{
			public const string Plain = "plain";
			public const string Sha256 = "S256";
		}

		public static class ProtectedResourceErrors
		{
			public const string InvalidToken = "invalid_token";
			public const string ExpiredToken = "expired_token";
			public const string InvalidRequest = "invalid_request";
			public const string InsufficientScope = "insufficient_scope";
		}

		public static class StandardScopes
		{
			/// <summary>REQUIRED. Informs the Authorization Server that the Client is making an OpenID Connect request. If the <c>openid</c> scope value is not present, the behavior is entirely unspecified.</summary>
			public const string OpenId = "openid";
			/// <summary>OPTIONAL. This scope value requests access to the End-User's default profile Claims, which are: <c>name</c>, <c>family_name</c>, <c>given_name</c>, <c>middle_name</c>, <c>nickname</c>, <c>preferred_username</c>, <c>profile</c>, <c>picture</c>, <c>website</c>, <c>gender</c>, <c>birthdate</c>, <c>zoneinfo</c>, <c>locale</c>, and <c>updated_at</c>.</summary>
			public const string Profile = "profile";
			/// <summary>OPTIONAL. This scope value requests access to the <c>email</c> and <c>email_verified</c> Claims.</summary>
			public const string Email = "email";
			/// <summary>OPTIONAL. This scope value requests access to the <c>address</c> Claim.</summary>
			public const string Address = "address";
			/// <summary>OPTIONAL. This scope value requests access to the <c>phone_number</c> and <c>phone_number_verified</c> Claims.</summary>
			public const string Phone = "phone";
			/// <summary>This scope value MUST NOT be used with the OpenID Connect Implicit Client Implementer's Guide 1.0. See the OpenID Connect Basic Client Implementer's Guide 1.0 (http://openid.net/specs/openid-connect-implicit-1_0.html#OpenID.Basic) for its usage in that subset of OpenID Connect.</summary>
			public const string OfflineAccess = "offline_access";
		}

		public static class AuthorizeErrors
		{
			// OAuth2 errors
			public const string InvalidRequest = "invalid_request";
			public const string UnauthorizedClient = "unauthorized_client";
			public const string AccessDenied = "access_denied";
			public const string UnsupportedResponseType = "unsupported_response_type";
			public const string InvalidScope = "invalid_scope";
			public const string ServerError = "server_error";
			public const string TemporarilyUnavailable = "temporarily_unavailable";

			// OIDC errors
			public const string InteractionRequired = "interaction_required";
			public const string LoginRequired = "login_required";
			public const string AccountSelectionRequired = "account_selection_required";
			public const string ConsentRequired = "consent_required";
			public const string InvalidRequestUri = "invalid_request_uri";
			public const string InvalidRequestObject = "invalid_request_object";
			public const string RequestNotSupported = "request_not_supported";
			public const string RequestUriNotSupported = "request_uri_not_supported";
			public const string RegistrationNotSupported = "registration_not_supported";

			// resource indicator spec
			public const string InvalidTarget = "invalid_target";
		}
	}
}