using System.Text;
using System.Text.Json;

namespace BHBackup.Client.Core;

internal static class LoginHelpers
{

    public static async Task<CoreApiCredentials> Authenticate(HttpClient httpClient, string username, string password, string deviceId)
    {
        var requestUri = $"{CoreApiClient.FamilyAppUri}/graphql?Authenticate";
        var requestBody = new {
            operationName = "Authenticate",
            variables = new {
                email = username,
                password = password,
                deviceId = deviceId,
                legacy = false
            },
            query = $@"
mutation Authenticate($email: EmailAddress!, $password: Password!, $deviceId: DeviceId, $legacy: Boolean) {{
  me {{
    authenticateWithPassword(
      email: $email
      password: $password
      deviceId: $deviceId
      legacy: $legacy
    ) {{
      ...AuthenticationResult
      __typename
    }}
    __typename
  }}
}}

fragment AuthenticationResult on AuthenticationResult {{
  status
  __typename
  ... on AuthenticationFailed {{
    status
    errorDetails
    errorTitle
    __typename
  }}
  ... on AuthenticationSucceeded {{
    accessToken
    deviceId
    __typename
  }}
  ... on AuthenticationChallenged {{
    ...AuthChallenge
    __typename
  }}
}}

fragment AuthChallenge on AuthenticationChallenged {{
  loginId
  deviceId
  expiresAt
  choices {{
    context {{
      ...UserContextFragment
      __typename
    }}
    hmac
    requiresTwoFactor
    __typename
  }}
  person {{
    name {{
      fullName
      __typename
    }}
    profileImage {{
      url
      __typename
    }}
    __typename
  }}
  __typename
}}

fragment UserContextFragment on UserContext {{
  id
  target {{
    __typename
    ... on PersonContextTarget {{
      person {{
        name {{
          fullName
          __typename
        }}
        __typename
      }}
      children {{
        name {{
          firstName
          fullName
          __typename
        }}
        profileImage {{
          url
          __typename
        }}
        __typename
      }}
      __typename
    }}
    ... on InstitutionSet {{
      title
      profileImage {{
        url
        __typename
      }}
      __typename
    }}
  }}
  __typename
}}"
        };

        var requestBodyJson = JsonSerializer.Serialize(requestBody);
        var requestContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(requestUri, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseContent);

        /*
        example response:

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

        */

        // use this as the "x-famly-accesstoken" header to authenticate future requests
        var accessToken = responseJson.RootElement
            .GetProperty("data")
            .GetProperty("me")
            .GetProperty("authenticateWithPassword")
            .GetProperty("accessToken")
            .GetString()
            ?? throw new InvalidOperationException();

        return new CoreApiCredentials(accessToken, deviceId);

    }

}
