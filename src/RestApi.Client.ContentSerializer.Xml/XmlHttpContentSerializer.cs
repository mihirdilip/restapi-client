// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RestApi.Client.ContentSerializer
{
	internal abstract class XmlHttpContentSerializerBase<T> : HttpContentSerializer<T> 
		where T : XmlHttpContentSerializerBase<T>
	{
		protected override Task<HttpContent> ProtectedGetHttpContentAsync<TRequestContent>(TRequestContent content)
		{
			var serializer = new XmlSerializer(typeof(TRequestContent));
			var serializedBody = new StringBuilder();
			using (var writer = XmlWriter.Create(serializedBody))
			{
				serializer.Serialize(writer, content);
			}
			HttpContent httpContent = new StringContent(serializedBody.ToString(), Encoding.UTF8, ContentMediaType);
			return Task.FromResult(httpContent);
		}

		protected override async Task<TResponseContent> ProtectedGetResponseContentAsync<TResponseContent>(HttpContent content)
		{
			var serializer = new XmlSerializer(typeof(TResponseContent));
			using (var reader = new StringReader(await content.ReadAsStringAsync().ConfigureAwait(false)))
			{
				return (TResponseContent)serializer.Deserialize(reader);
			}
		}
	}

	internal class TextXmlHttpContentSerializer : XmlHttpContentSerializerBase<TextXmlHttpContentSerializer>
	{
		public override string ContentMediaType { get; } = MediaMimeTypes.Text.Xml;
	}

	internal class ApplicationXmlHttpContentSerializer : XmlHttpContentSerializerBase<ApplicationXmlHttpContentSerializer>
	{
		public override string ContentMediaType { get; } = MediaMimeTypes.Application.Xml;
	}
}
