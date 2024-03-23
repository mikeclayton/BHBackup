using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Client.GraphQl.Identity.Queries;
using BHBackup.Common.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl.Identity;

public static class IdentityExtensions
{

    public static async Task<AuthenticateResponse> Authenticate(
        this GraphQlClient graphQlClient,
        string username,
        string password,
        string deviceId
    )
    {
        var requestUri = $"/graphql?{nameof(Authenticate)}";
        var requestBody = new
        {
            operationName = "Authenticate",
            variables = new
            {
                email = username,
                password = password,
                deviceId = deviceId,
                legacy = false
            },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                $"{typeof(IdentityQueryResources).Namespace}.{nameof(Authenticate)}.graphql"
            )
        };
        /*
        response:

        {
          "data":{
            "me":{
              "authenticateWithPassword":{
                "status":"Succeeded",
                "__typename":"AuthenticationSucceeded",
                "accessToken":"<some_guid>",
                "deviceId":"<some_guid>"
              },
              "__typename":"MeMutation"
            }
          }
        }

        or

        {
          "data": {
            "me": {
              "authenticateWithPassword": {
                "status": "FailedInvalidPassword",
                "__typename": "AuthenticationFailed",
                "errorDetails": "The provided password and email did not match",
                "errorTitle": "Invalid password"
              },
              "__typename": "MeMutation"
            }
          }
        }

         */
        return await graphQlClient.ExecuteGraphQlRequest<AuthenticateResponse>(
            requestUrl: $"/graphql?{nameof(Authenticate)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );
    }

    public static async Task<GetCurrentContextResponse> GetCurrentContext(
        this GraphQlClient graphQlClient
    )
    {

        var requestUri = $"/graphql?{nameof(GetCurrentContext)}";
        var requestBody = new
        {
            operationName = nameof(GetCurrentContext),
            variables = new { },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                $"{typeof(IdentityQueryResources).Namespace}.{nameof(GetCurrentContext)}.graphql"
            )
        };

        return await graphQlClient.ExecuteGraphQlRequest<GetCurrentContextResponse>(
            requestUrl: $"/graphql?{nameof(GetCurrentContext)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );

    }

}
