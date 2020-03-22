// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// API Key authentication provider. The implementation of this interface should passed as a type parameter when adding API Key authentication to the <see cref="IRestClientBuilder"/>.
	/// </summary>
	public interface IApiKeyAuthenticationProvider
	{
		/// <summary>
		/// Provides the pipeline with an instance of <see cref="ApiKeyAuthentication"/> to be used for authenticating the request.
		/// </summary>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
		/// <returns>An instance of <see cref="ApiKeyAuthentication"/>.</returns>
		Task<ApiKeyAuthentication> ProvideAsync(CancellationToken cancellationToken);
	}
}
