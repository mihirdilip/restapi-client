// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public interface IRestTokenClient
	{
		/// <summary>
		/// Using the default token provider set up in the pipeline,
		/// it will process the <paramref name="request"/> of type
		/// <see cref="ClientCredentialsTokenRequest"/> or
		/// <see cref="PasswordCredentialsTokenRequest"/> or
		/// <see cref="RefreshTokenRequest"/> or
		/// <see cref="RevokeTokenRequest"/>
		/// and return the <see cref="TokenResponse"/>.
		/// </summary>
		/// <param name="request">
		/// <para><see cref="ClientCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with client credentials token.</para>
		/// <para><see cref="PasswordCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with password credentials token.</para>
		/// <para><see cref="RefreshTokenRequest"/> <paramref name="request"/> will refresh the token if supported by provider and return the <see cref="TokenResponse"/> with refreshed token.</para>
		/// <para><see cref="RevokeTokenRequest"/> <paramref name="request"/> will revoke the token from the provider.</para>
		/// </param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
		/// <returns>The <see cref="TokenResponse"/>.</returns>
		Task<TokenResponse> RequestTokenAsync(ITokenRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Using the specific token provider with <paramref name="providerName"/> set up in the pipeline,
		/// it will process the <paramref name="request"/> of type
		/// <see cref="ClientCredentialsTokenRequest"/> or
		/// <see cref="PasswordCredentialsTokenRequest"/> or
		/// <see cref="RefreshTokenRequest"/> or
		/// <see cref="RevokeTokenRequest"/>
		/// and return the <see cref="TokenResponse"/>.
		/// </summary>
		/// <param name="providerName">The token provider name.</param>
		/// <param name="request">
		/// <para><see cref="ClientCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with client credentials token.</para>
		/// <para><see cref="PasswordCredentialsTokenRequest"/> <paramref name="request"/> will return the <see cref="TokenResponse"/> with password credentials token.</para>
		/// <para><see cref="RefreshTokenRequest"/> <paramref name="request"/> will refresh the token if supported by provider and return the <see cref="TokenResponse"/> with refreshed token.</para>
		/// <para><see cref="RevokeTokenRequest"/> <paramref name="request"/> will revoke the token from the provider.</para>
		/// </param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
		/// <returns>The <see cref="TokenResponse"/>.</returns>
		Task<TokenResponse> RequestTokenAsync(string providerName, ITokenRequest request, CancellationToken cancellationToken = default);
	}
}
