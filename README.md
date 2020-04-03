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

You can also add your own custom content serializer to the pipeline. Please refer [Extending](#extending) section.

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

You can also add your own authentication handler to the pipeline. Please refer [Extending](#extending) section.

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

Note: If for any reason if you get a **circular dependency errors**, use `IRestTokenClient` instead of `IRestClient` for requesting token. 


## Extending

## Example Usage

Example usage for all the available Authentication types can be found here.
[authentication-clientside-samples](https://github.com/mihirdilip/authentication-clientside-samples)



## License
[MIT License](https://github.com/mihirdilip/restapi-client/blob/master/LICENSE)