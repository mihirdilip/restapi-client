# RestApi.Client
Rest API client for easy access to restful API within your application. It is developed on netstandard2.0 and it is very extensible so custom Authentication and ContentSerializer can be easily added to the pipeline.

[View On GitHub](https://github.com/mihirdilip/restapi-client)

## Installing
This library is published on NuGet. So the NuGet package can be installed directly to your project if you wish to use it without making any custom changes to the code.

Download directly from [RestApi.Client](https://www.nuget.org/packages/RestApi.Client).

Or by running the below command on your project.

```
PM> Install-Package RestApi.Client
```

#### Content Serializer
In addition to the above `RestApi.Client` package, you will need to also install relavant NuGet package for handling content serialization depending on your requirement. This is basically the content media-types *RestApi.Client* can handle when sending an API request and handling an API response content. All the supported ContentSerializer package name starts with `RestApi.Client.ContentSerializer.*` 

Currently supported content serializers along with link to NuGet are: 
- Json ([RestApi.Client.ContentSerializer.Json](https://www.nuget.org/packages/RestApi.Client.ContentSerializer.Json))
- PlainText ([RestApi.Client.ContentSerializer.PlainText](https://www.nuget.org/packages/RestApi.Client.ContentSerializer.PlainText))
- Xml ([RestApi.Client.ContentSerializer.Xml](https://www.nuget.org/packages/RestApi.Client.ContentSerializer.Xml))

Command to install directly to your project.

```sh
PM> Install-Package RestApi.Client.ContentSerializer.<ContentType>

# where: <ContentType> is any of the above listed type.
```

By Default, `RestApi.Client` already comes with [`Json`](https://www.nuget.org/packages/RestApi.Client.ContentSerializer.Json) and [`PlainText`](https://www.nuget.org/packages/RestApi.Client.ContentSerializer.PlainText) content serializers as these are commonly used. So there is no need to install them separately.

You can also add your own custom content serializer to the pipeline. Please refer [Example Usage](#Example Usage) section.

Note: Only downloading ContentSerializer package without *RestApi.Client* will be of no use so make sure you have downloaded *RestApi.Client* package as well.

#### Authentication
In addition to the above `RestApi.Client` package, you will need to also install relavant NuGet package for authentication depending on your requirement. All the supported authentication package name starts with `RestApi.Client.Authentication.*` 

Currently supported authentication types along with link to NuGet are: 
- ApiKey ([RestApi.Client.Authentication.ApiKey](https://www.nuget.org/packages/RestApi.Client.Authentication.ApiKey))
- Basic ([RestApi.Client.Authentication.Basic](https://www.nuget.org/packages/RestApi.Client.Authentication.Basic))
- Bearer ([RestApi.Client.Authentication.Bearer](https://www.nuget.org/packages/RestApi.Client.Authentication.Bearer))
- OAuth2 ([RestApi.Client.Authentication.OAuth2](https://www.nuget.org/packages/RestApi.Client.Authentication.OAuth2))
- Windows ([RestApi.Client.Authentication.Windows](https://www.nuget.org/packages/RestApi.Client.Authentication.Windows))

Command to install directly to your project.

```sh
PM> Install-Package RestApi.Client.Authentication.<AuthenticationType>

# where: <AuthenticationType> is any of the above listed type.
```

You can also add your own authentication handler to the pipeline. Please refer [Example Usage](#Example Usage) section.

Note: Only downloading Authentication package without *RestApi.Client* will be of no use so make sure you have downloaded *RestApi.Client* package as well.

#### Authentication - Token Extensions
This extention adds support for retrieving Access/Bearer token from OAuth2 provider. It comes with caching facility which also handles the refreshing of token before or when it expires. It is useful when you have to get a ClientCredential or PasswordCredential token from OAuth2 provider. Usually used when using OAuth2 or Bearer authentication.

Command to install directly to your project.
```
PM> Install-Package RestApi.Client.Authentication.TokenExtensions
```

This adds below to methods to the `IRestClient`
```c#
Task<TokenResponse> RequestTokenAsync(ITokenRequest request, CancellationToken cancellationToken = default);

Task<TokenResponse> RequestTokenAsync(string providerName, ITokenRequest request, CancellationToken cancellationToken = default);
```

Note: If for any reason if you get **circular dependency errors**, use `IRestTokenClient` instead of `IRestClient` for requesting token. 


## Example Usage

Setting and using RestClient is quite simple. You will need basic working knowledge of .NET Core to get started using this code.

Example usages can be found here..
[authentication-clientside-samples](https://github.com/mihirdilip/authentication-clientside-samples)

#### ASP.NET Core Example
When using with ASP.NET Core we can use the extension method for adding `IRestClient` to the depencencies in the ConfigureServices method of StartUp class.

```C#

// ... (3 dots) represents code is removed to keep it concise

using RestApi.Client;
public class Startup
{
	// ... 

	public void ConfigureServices(IServiceCollection services)
	{

		// The below adds IRestClient implementaion as singleton to the IOC dependencies.
		// IRestClient can be resolved from the dependencies which is then used to make rest api calls. 
		services.AddRestClient(clientBuilder =>
		{
			// Optional but recommended. To set BaseUrl of the api.
			// If it is set here then you will only need to provide the rest of the resource uri with every call.
			// If it is not set here, you will have to use whole url with every call.
			clientBuilder.SetBaseAddress(new Uri(Constants.BaseUrl))

			// Optional. To set the default request headers to be used with every call. 
			.SetDefaultRequestHeaders(new RestHttpHeaders { { "header_key", "header_value" } })



			// Below are the content serializers you may want to add to the pipeline.
			// You will need to install relevant NuGet package(RestApi.Client.ContentSerializer.* ) to be able to add the content serializer you want to use.
			// The below 2 content serializers are already added by default as they are commonly used for api so no need to add them again. 
			.AddPlainTextHttpContentSerializer()
			.AddJsonHttpContentSerializer()

			// Optional. Add the below line if the api needs application/xml, text/xml as request content type and/or response content type.
			// You will need to install RestApi.Client.ContentSerializer.Xml NuGet package to be able to add this.
			.AddXmlHttpContentSerializer()

			// Optional. To add your custom http content serializer which is an implementation of IHttpContentSerializer to the pipeline. 
			// Only use this if the content serializer is not available from NuGet repository for the content type you want to support.
			.AddHttpContentSerializer<YourCustomHttpContentSerializerImplementation>()




			// Below are the authentication handlers you may want to add to the pipeline.
			// Only one handler (one that is last added) will be used.
			// It is optional but API always have some sort of authentication mechanism so you will need at least one handler depending on the API 
			// You will need to install relevant NuGet package (RestApi.Client.Authentication.* ) to be able to add the handler you want to use.


			// To add Windows authentication using the default credentials of the user logged on the PC.
			// Install RestApi.Client.Authentication.Windows NuGet package to use this.
			.AddWindowsDefaultAuthentication()

			// To add APIKey authentication. Takes an api key authentication provider which is an implementation of IApiKeyAuthenticationProvider.
			// Install RestApi.Client.Authentication.ApiKey NuGet package to use either of the below.
			// This one adds api key as request header for every call. 
			.AddApiKeyInHeaderAuthentication<ApiKeyAuthenticationProvider>()
			// This one adds api key as url query parameter for every call.
			.AddApiKeyInQueryParamsAuthentication<ApiKeyAuthenticationProvider>()

			// To add Basic authentication. Takes a basic authentication provider which is an implementation of IBasicAuthenticationProvider.
			// Install RestApi.Client.Authentication.Basic NuGet package to use this.
			.AddBasicAuthentication<BasicAuthenticationProvider>()

			// To add Bearer token authentication. Takes a bearer authentication provider which is an implementation of IBearerAuthenticationProvider.
			// Install RestApi.Client.Authentication.Bearer NuGet package to use this.
			.AddBearerAuthentication<BearerAuthenticationProvider>()

			// To add OAuth2 authentication. Takes a oauth 2.0 authentication provider which is an implementation of IOAuth2AuthenticationProvider.
			// Install RestApi.Client.Authentication.OAuth2 NuGet package to use this.
			.AddOAuth2Authentication<OAuth2AuthenticationProvider>()

			// Optional. To add your custom Authentication Handler which is an implementation of IRestAuthenticationHandler to the pipeline. 
			// Only use this if the authentication handler is not available from NuGet repository for what you want to support.
			.AddAuthenticationHandler<YourCustomAuthenticationHandlerImplementation>()





			// Authentication - Token Extension
			// To add token provider and ability to use token client add the below to the pipeline.
			// You can either add Password Credentials or Client Credentials as config depending on which type of authentication flow you need to support.
			// You and then utilize IRestTokenClient to get request access token from the provider. IRestTokenClient handles all the caching and refreshing the token on expiry.
			// Install RestApi.Client.Authentication.TokenExtensions NuGet package to use this.
			.AddTokenProvider(new PasswordCredentialsTokenProviderConfig
			{
				TokenRequestEndpointAddress = Constants.BaseUrl + "/id/connect/token",
				TokenRevocationEndpointAddress = Constants.BaseUrl + "/id/connect/revocation",
				ClientId = "console-client",
				ClientSecret = "console-client-secret",
				Scope = "api"
			})

			.AddTokenProvider(new ClientCredentialsTokenProviderConfig
			{
				TokenRequestEndpointAddress = Constants.BaseUrl + "/id/connect/token",
				TokenRevocationEndpointAddress = Constants.BaseUrl + "/id/connect/revocation",
				ClientId = "console-client",
				ClientSecret = "console-client-secret",
				Scope = "api"
			})

			;
		});


		// ... 
	}

	// ... 
}


```

#### Console or Standalone App Example
When using with Console or other standalone apps, you can build an instance of `IRestClient` using `RestClientBuilder`.

```C#

using RestApi.Client;
using RestApi.Client.Authentication;
class Program
{
	private const ApiKeyIn ApiKeyIn = ClientSide.ApiKeyIn.Header;

	static async Task Main(string[] args)
	{
		// RestClientBuilder is a fluent builder for building the rest api client
		// which is the core of this library and is used to make all the rest api requests.
		var clientBuilder = new RestClientBuilder()
				
			// Optional but recommended. To set BaseUrl of the api.
			// If it is set here then you will only need to provide the rest of the resource uri with every call.
			// If it is not set here, you will have to use whole url with every call.
			.SetBaseAddress(new Uri(Constants.BaseUrl))

			// Optional. To set the default request headers to be used with every call. 
			.SetDefaultRequestHeaders(new RestHttpHeaders {{"header_key", "header_value"}})



			// Below are the content serializers you may want to add to the pipeline.
			// You will need to install relevant NuGet package(RestApi.Client.ContentSerializer.* ) to be able to add the content serializer you want to use.
			// The below 2 content serializers are already added by default as they are commonly used for api so no need to add them again. 
			.AddPlainTextHttpContentSerializer()
			.AddJsonHttpContentSerializer()

			// Optional. Add the below line if the api needs application/xml, text/xml as request content type and/or response content type.
			// You will need to install RestApi.Client.ContentSerializer.Xml NuGet package to be able to add this.
			.AddXmlHttpContentSerializer()

			// Optional. To add your custom http content serializer which is an implementation of IHttpContentSerializer to the pipeline. 
			// Only use this if the content serializer is not available from NuGet repository for the content type you want to support.
			.AddHttpContentSerializer<YourCustomHttpContentSerializerImplementation>()




			// Below are the authentication handlers you may want to add to the pipeline.
			// Only one handler (one that is last added) will be used.
			// It is optional but API always have some sort of authentication mechanism so you will need at least one handler depending on the API 
			// You will need to install relevant NuGet package (RestApi.Client.Authentication.* ) to be able to add the handler you want to use.


			// To add Windows authentication using the default credentials of the user logged on the PC.
			// Install RestApi.Client.Authentication.Windows NuGet package to use this.
			.AddWindowsDefaultAuthentication()

			// To add APIKey authentication. Takes an api key authentication provider which is an implementation of IApiKeyAuthenticationProvider.
			// Install RestApi.Client.Authentication.ApiKey NuGet package to use either of the below.
			// This one adds api key as request header for every call. 
			.AddApiKeyInHeaderAuthentication<ApiKeyAuthenticationProvider>()
			// This one adds api key as url query parameter for every call.
			.AddApiKeyInQueryParamsAuthentication<ApiKeyAuthenticationProvider>()

			// To add Basic authentication. Takes a basic authentication provider which is an implementation of IBasicAuthenticationProvider.
			// Install RestApi.Client.Authentication.Basic NuGet package to use this.
			.AddBasicAuthentication<BasicAuthenticationProvider>()

			// To add Bearer token authentication. Takes a bearer authentication provider which is an implementation of IBearerAuthenticationProvider.
			// Install RestApi.Client.Authentication.Bearer NuGet package to use this.
			.AddBearerAuthentication<BearerAuthenticationProvider>()

			// To add OAuth2 authentication. Takes a oauth 2.0 authentication provider which is an implementation of IOAuth2AuthenticationProvider.
			// Install RestApi.Client.Authentication.OAuth2 NuGet package to use this.
			.AddOAuth2Authentication<OAuth2AuthenticationProvider>()

			// Optional. To add your custom Authentication Handler which is an implementation of IRestAuthenticationHandler to the pipeline. 
			// Only use this if the authentication handler is not available from NuGet repository for what you want to support.
			.AddAuthenticationHandler<YourCustomAuthenticationHandlerImplementation>()





			// Authentication - Token Extension
			// To add token provider and ability to use token client add the below to the pipeline.
			// You can either add Password Credentials or Client Credentials as config depending on which type of authentication flow you need to support.
			// You and then utilize IRestTokenClient to get request access token from the provider. IRestTokenClient handles all the caching and refreshing the token on expiry.
			// Install RestApi.Client.Authentication.TokenExtensions NuGet package to use this.
			.AddTokenProvider(new PasswordCredentialsTokenProviderConfig
			{
				TokenRequestEndpointAddress = Constants.BaseUrl + "/id/connect/token",
				TokenRevocationEndpointAddress = Constants.BaseUrl + "/id/connect/revocation",
				ClientId = "console-client",
				ClientSecret = "console-client-secret",
				Scope = "api"
			})

			.AddTokenProvider(new ClientCredentialsTokenProviderConfig
			{
				TokenRequestEndpointAddress = Constants.BaseUrl + "/id/connect/token",
				TokenRevocationEndpointAddress = Constants.BaseUrl + "/id/connect/revocation",
				ClientId = "console-client",
				ClientSecret = "console-client-secret",
				Scope = "api"
			})
		;

		
		// Build() methods with build the above builder and return `IRestClient`
		var client = clientBuilder.Build();

		// client can then be used to make rest api calls. Can also be added to any IOC container you may be using as singleton instance.

		Console.ReadKey();
	}
}

// ... represents code is removed to keep it concise
```

## License
[MIT License](https://github.com/mihirdilip/restapi-client/blob/master/LICENSE)