// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using RestApi.Client.Authentication;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client
{
	internal class NullRestAuthenticationHandler : IRestAuthenticationHandler
	{
		public Task<bool> HandleAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(true);
		}
	}

	internal static class NullAuthenticationExtensions
	{
		public static IRestClientBuilder AddNullAuthentication(this IRestClientBuilder builder)
		{
			builder.AddAuthenticationHandler<NullRestAuthenticationHandler>();
			return builder;
		}
	}
}
