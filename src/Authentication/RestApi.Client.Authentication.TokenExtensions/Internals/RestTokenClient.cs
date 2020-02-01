// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	internal class RestTokenClient : IRestTokenClient
	{
		private readonly ITokenService _tokenService;

		public RestTokenClient(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		public Task<TokenResponse> RequestTokenAsync(ITokenRequest request, CancellationToken cancellationToken = default)
		{
			return RequestTokenAsync(Constants.DefaultTokenProviderName, request, cancellationToken);
		}

		public Task<TokenResponse> RequestTokenAsync(string providerName, ITokenRequest request, CancellationToken cancellationToken = default)
		{
			return _tokenService.ProcessRequestAsync(providerName, request, cancellationToken);
		}
	}
}
