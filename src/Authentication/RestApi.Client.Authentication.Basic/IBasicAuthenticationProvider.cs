// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// Basic authentication provider. The implementation of this interface should passed as a type parameter when adding Basic authentication to the <see cref="IRestClientBuilder"/>.
	/// </summary>
	public interface IBasicAuthenticationProvider
	{
		/// <summary>
		/// Provides the pipeline with an instance of <see cref="BasicAuthentication"/> to be used for authenticating the request.
		/// </summary>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
		/// <returns>An instance of <see cref="BasicAuthentication"/>.</returns>
		Task<BasicAuthentication> ProvideAsync(CancellationToken cancellationToken);
	}
}
