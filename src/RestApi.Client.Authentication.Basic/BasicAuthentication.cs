// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

using Mihir.AspNetCore.Authentication.Basic;
using System;
using System.Text;

namespace RestApi.Client.Authentication
{
	public class BasicAuthentication
	{
		public string Scheme { get; }
		public string Value { get; }

		public BasicAuthentication(string username, string password)
			: this(Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}")))
		{

		}

		public BasicAuthentication(string token)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

			Scheme = BasicDefaults.AuthenticationScheme;
			Value = token;
		}

		protected bool Equals(BasicAuthentication other)
		{
			return string.Equals(Scheme, other.Scheme) && string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((BasicAuthentication)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Scheme != null ? Scheme.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
			}
		}
	}
}
