// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
    internal class JsonHttpContentSerializer : HttpContentSerializer<JsonHttpContentSerializer>
	{
		public override string ContentMediaType { get; } = MediaMimeTypes.Application.Json;
		protected override Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			HttpContent httpContent = new StringContent(
				JsonSerializer.Serialize(content), 
				Encoding.UTF8, 
				ContentMediaType
			);
			return Task.FromResult(httpContent);
		}

		protected override async Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			return JsonSerializer.Deserialize<TResponseContent>(
				await content.ReadAsStringAsync().ConfigureAwait(false)
			);
		}
	}
}
