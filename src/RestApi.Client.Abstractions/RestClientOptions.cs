// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using RestApi.Client.ContentSerializer;
using System;
using System.Collections.Generic;

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


		internal readonly List<IHttpContentSerializer> HttpContentSerializers = new();
		internal readonly List<Type> HttpContentSerializerImplementationTypes = new();
		
		public void AddHttpContentSerializer<THttpContentSerializerImplementation>()
			where THttpContentSerializerImplementation : class, IHttpContentSerializer
		{
			var type = typeof(THttpContentSerializerImplementation);
			if (!HttpContentSerializerImplementationTypes.Contains(type))
			{
				HttpContentSerializerImplementationTypes.Add(type);
			}
		}

		public void AddHttpContentSerializer(IHttpContentSerializer httpContentSerializer)
		{
			if (httpContentSerializer == null) throw new ArgumentNullException(nameof(httpContentSerializer));
			if (!HttpContentSerializers.Contains(httpContentSerializer))
			{
				HttpContentSerializers.Add(httpContentSerializer);
			}
		}

		public void ClearHttpContentSerializers()
		{
			HttpContentSerializerImplementationTypes.Clear();
			HttpContentSerializers.Clear();
		}


		internal readonly List<Type> RestClientValidatorImplementationTypes = new();
		internal readonly List<IRestClientValidator> RestClientValidators = new();
		public void AddValidator<TRestClientValidatorImplementation>()
			where TRestClientValidatorImplementation : class, IRestClientValidator
		{
			var type = typeof(TRestClientValidatorImplementation);
			if (!RestClientValidatorImplementationTypes.Contains(type))
			{
				RestClientValidatorImplementationTypes.Add(type);
			}
		}

		public void AddValidator(IRestClientValidator validator)
		{
			if (validator == null) throw new ArgumentNullException(nameof(validator));
			if (!RestClientValidators.Contains(validator))
			{
				RestClientValidators.Add(validator);
			}
		}

		internal void CopyFrom(RestClientOptions options)
		{
			BaseAddress = options.BaseAddress;
			DefaultRequestHeaders = options.DefaultRequestHeaders;
			options.HttpContentSerializers.ForEach(i => HttpContentSerializers.Add(i));
			options.HttpContentSerializerImplementationTypes.ForEach(i => HttpContentSerializerImplementationTypes.Add(i));
			options.RestClientValidators.ForEach(i => RestClientValidators.Add(i));
			options.RestClientValidatorImplementationTypes.ForEach(i => RestClientValidatorImplementationTypes.Add(i));
		}
	}
}
