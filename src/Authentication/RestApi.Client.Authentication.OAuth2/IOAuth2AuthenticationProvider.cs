// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// OAuth 2.0 authentication provider. The implementation of this interface should passed as a type parameter when adding OAuth 2.0 authentication to the <see cref="IRestClientBuilder"/>.
	/// </summary>
	public interface IOAuth2AuthenticationProvider
	{
		/// <summary>
		/// Provides the pipeline with an instance of <see cref="OAuth2Authentication"/> to be used for authenticating the request.
		/// </summary>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
		/// <returns>An instance of <see cref="OAuth2Authentication"/>.</returns>
		Task<OAuth2Authentication> ProvideAsync(CancellationToken cancellationToken);
	}
}
