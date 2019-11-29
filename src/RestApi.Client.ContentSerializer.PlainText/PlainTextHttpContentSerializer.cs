// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	internal class PlainTextHttpContentSerializer : HttpContentSerializer<PlainTextHttpContentSerializer>
	{
		public override string ContentMediaType { get; } = MediaMimeTypes.Text.Plain;
		protected override Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			HttpContent httpContent = new StringContent(Convert.ToString(content), Encoding.UTF8, ContentMediaType);
			return Task.FromResult(httpContent);
		}

		protected override async Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			var responseContent = await content.ReadAsStringAsync().ConfigureAwait(false);
			return (TResponseContent)Convert.ChangeType(responseContent, typeof(TResponseContent));
		}
	}
}
