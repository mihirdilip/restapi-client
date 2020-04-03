// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	/// <summary>
	/// An interface to be implemented for added an authentication handler to the pipeline.
	/// </summary>
	public interface IRestAuthenticationHandler
	{
		/// <summary>
		/// Handles the authentication by adding the required parameters to the request pipeline. It can be request headers, query string to the url, etc.
		/// </summary>
		/// <param name="request">The <see cref="HttpRequestMessage"/> to which the required parameters will be added.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
		/// <returns>Return a boolean indicating that it is handled.</returns>
		Task<bool> HandleAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);
	}
}