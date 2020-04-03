// Copyright (c) Mihir Dilip. All rights reserved.
// Licensed under the MIT License. See License in the project root for license information.

namespace RestApi.Client
{
	/// <summary>
	/// An interface to be implemented for adding rest client validator to the <see cref="IRestClient"/> pipeline. It validates <see cref="IRestClient"/> when creating it's instance.
	/// </summary>
	public interface IRestClientValidator
	{
		/// <summary>
		/// Implementation of validation of <see cref="IRestClient"/> to be done when creating and instance of <see cref="IRestClient"/>.
		/// </summary>
		void Validate();
	}
}