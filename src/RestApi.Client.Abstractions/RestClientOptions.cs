// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client
{
	/// <summary>
	/// Options for <see cref="IRestClient"/>
	/// </summary>
	public class RestClientOptions 
	{
		/// <summary>
		/// Create an instance of <see cref="RestClientOptions"/>.
		/// </summary>
		public RestClientOptions()
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="RestClientOptions"/> with base API address and optionally with default request <see cref="RestHttpHeaders"/> to be used with all the requests made by <see cref="IRestClient"/>.
		/// </summary>
		/// <param name="baseAddress">The base API address to be used with all the requests.</param>
		/// <param name="defaultRequestHeaders">The default request headers <see cref="RestHttpHeaders"/> to be used with all the requests.</param>
		public RestClientOptions(Uri baseAddress, RestHttpHeaders defaultRequestHeaders = null)
		{
			BaseAddress = baseAddress;
			DefaultRequestHeaders = defaultRequestHeaders ?? new RestHttpHeaders();
		}

		private Uri _baseAddress;

		/// <summary>
		/// Gets or sets the base API address to be used by all the requests made by <see cref="IRestClient"/>.
		/// </summary>
		public Uri BaseAddress
		{
			get => _baseAddress;
			set
			{
				var uri = value?.ToString();
				if (!string.IsNullOrWhiteSpace(uri) && !uri.EndsWith("/"))
				{
					uri += "/";
					_baseAddress = new Uri(uri);
				}
				else
				{
					_baseAddress = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the default request headers to be used by all the requests made by <see cref="IRestClient"/>.
		/// </summary>
		public RestHttpHeaders DefaultRequestHeaders { get; set; } = new RestHttpHeaders();
	}
}
