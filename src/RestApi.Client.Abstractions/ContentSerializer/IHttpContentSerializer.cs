// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi.Client.ContentSerializer
{
	public interface IHttpContentSerializer
	{
		string ContentMediaType { get; }
		Task<HttpContent> GetHttpContentAsync<TRequestContent>(TRequestContent content);
		Task<TResponseContent> GetResponseContentAsync<TResponseContent>(HttpContent content);
	}
}