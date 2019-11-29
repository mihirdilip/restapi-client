// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;

namespace RestApi.Client
{
	public class RestClientOptions
	{
		private static readonly int DefaultMaxResponseContentBufferSize = int.MaxValue;
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);
		private static readonly TimeSpan MaxTimeout = TimeSpan.FromMilliseconds(int.MaxValue);
		private static readonly TimeSpan InfiniteTimeout = System.Threading.Timeout.InfiniteTimeSpan;

		private int _maxResponseContentBufferSize = DefaultMaxResponseContentBufferSize;
		private TimeSpan _timeout = DefaultTimeout;

		public Uri BaseAddress { get; set; }
		public RestHttpHeaders DefaultRequestHeaders { get; set; } = new RestHttpHeaders();

		public int MaxResponseContentBufferSize
		{
			get => _maxResponseContentBufferSize;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				_maxResponseContentBufferSize = value;
			}
		}

		public TimeSpan Timeout
		{
			get => _timeout;
			set
			{
				if (value != InfiniteTimeout && (value <= TimeSpan.Zero || value > MaxTimeout))
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}
				_timeout = value;
			}
		}
	}
}
