// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client
{
	public class RestRequestContent<TContent> 
	{
		public RestRequestContent(TContent content, string contentMediaType = default)
		{
			Content = content;
			ContentMediaType = contentMediaType;
		}

		public TContent Content { get; }
		public string ContentMediaType { get; }
	}
}
