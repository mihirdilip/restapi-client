// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client
{
	public interface IRestClientFactory
	{
		IRestClient CreateClient(string name);
		
		TClient CreateClient<TClient>(string name = null) 
			where TClient : class;

		TClient CreateClient<TClient, TImplementation>(string name = null)
			where TClient : class
			where TImplementation : class, TClient;
	}
}
