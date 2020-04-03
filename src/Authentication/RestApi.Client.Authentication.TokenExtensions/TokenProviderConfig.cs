// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// An abstract token provider config.
	/// </summary>
	public abstract class TokenProviderConfig 
	{
		protected TokenProviderConfig(string grantType)
		{
			GrantType = grantType;
		}

		/// <summary>
		/// Gets the grant type. 
		/// </summary>
		public string GrantType { get; }

		/// <summary>
		/// Gets or sets the token request endpoint address.
		/// </summary>
		public string TokenRequestEndpointAddress { get; set; }

		/// <summary>
		/// Gets or sets the token revocation endpoint address. If not set, token request endpoint address will be used when revoking token.
		/// </summary>
		public string TokenRevocationEndpointAddress { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		public string ClientId { get; set; }

		/// <summary>
		/// Gets or sets the client secret.
		/// </summary>
		public string ClientSecret { get; set; }

		/// <summary>
		/// Gets or sets space separated list of the default scope for the provider.
		/// </summary>
		public string Scope { get; set; }

		/// <summary>
		/// Gets or sets the client credential method <see cref="ClientAuthenticationMethod"/>. Default value is 'HeaderBasicAuthenticationOAuth2Spec'.
		/// </summary>
		public ClientAuthenticationMethod ClientAuthenticationMethod { get; set; } = ClientAuthenticationMethod.HeaderBasicAuthenticationOAuth2Spec;

		internal string ProviderName { get; set; }
	}

	/// <summary>
	/// Client Credentials token provide config.
	/// </summary>
	public class ClientCredentialsTokenProviderConfig : TokenProviderConfig
	{
		public ClientCredentialsTokenProviderConfig() 
			: base(OidcConstants.GrantTypes.ClientCredentials)
		{
		}
	}

	/// <summary>
	/// Password Credentials token provider config.
	/// </summary>
	public class PasswordCredentialsTokenProviderConfig : TokenProviderConfig
	{
		public PasswordCredentialsTokenProviderConfig() 
			: base(OidcConstants.GrantTypes.Password)
		{
		}
	}
}
