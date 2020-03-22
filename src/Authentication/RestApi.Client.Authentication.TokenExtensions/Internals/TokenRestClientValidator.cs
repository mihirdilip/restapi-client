// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Client.Authentication
{
	internal class TokenRestClientValidator : IRestClientValidator
	{
		private readonly IEnumerable<TokenProviderConfig> _tokenProviderConfigs;

		public TokenRestClientValidator(IEnumerable<TokenProviderConfig> tokenProviderConfigs)
		{
			_tokenProviderConfigs = tokenProviderConfigs;
		}

		public void Validate()
		{
			var groupedConfigs = _tokenProviderConfigs.GroupBy(o => o.ProviderName);
			foreach (var group in groupedConfigs)
			{
				if (group.Count() > 1)
				{
					throw new InvalidOperationException($"Multiple {nameof(TokenProviderConfig)} is not support for a same provider name '{group.Key}'.");
				}
			}
		}
	}
}
