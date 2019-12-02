// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using RestApi.Client.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using RestApi.Client.ContentSerializer;

namespace RestApi.Client.Sample.ConsoleApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var client = new RestClientBuilder()
				.SetBaseAddress(new Uri("https://localhost/api/v1"))
				.AddBasicAuthentication<BasicAuthenticationProvider>()
				.Build();
			var res = await client.GetAsync<List<Hub>>("hubs").ConfigureAwait(false);

			Console.ReadKey();
		}
	}

	internal class BasicAuthenticationProvider : IBasicAuthenticationProvider
	{
		public Task<BasicAuthentication> ProvideAsync()
		{
			return Task.FromResult(new BasicAuthentication("", ""));
		}
	}

	public class Hub
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
