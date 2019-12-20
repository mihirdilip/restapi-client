// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	internal class JsonHttpContentSerializer : HttpContentSerializer<JsonHttpContentSerializer>
	{
		public override string ContentMediaType { get; } = MediaMimeTypes.Application.Json;
		protected override Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(content), Encoding.UTF8, ContentMediaType
			);
			return Task.FromResult(httpContent);
		}

		protected override async Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			return JsonConvert.DeserializeObject<TResponseContent>(
				await content.ReadAsStringAsync().ConfigureAwait(false)
			);
		}
	}
}
