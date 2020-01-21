// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public interface IRestTokenClient
	{
		Task<TokenResponse> RequestTokenAsync(ITokenRequest request);
		Task<TokenResponse> RequestTokenAsync(string providerName, ITokenRequest request);
	}
}
