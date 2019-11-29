// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.Authentication
{
	public interface IRestAuthenticationHandler
	{
		Task<bool> HandleAsync(HttpRequestMessage request);
	}
}