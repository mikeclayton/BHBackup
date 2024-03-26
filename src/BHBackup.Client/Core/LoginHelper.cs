using BHBackup.Client.GraphQl;
using BHBackup.Client.GraphQl.Identity;
using BHBackup.Client.GraphQl.Identity.Models.Authentication;

namespace BHBackup.Client.Core;

public static class LoginHelper
{

    public static async Task<CoreApiCredentials> Authenticate(HttpClient httpClient, string username, string password, string deviceId)
    {
        var graphQlClient = new GraphQlClient(httpClient);
        var response = await graphQlClient.Authenticate(username, password, deviceId).ConfigureAwait(false);
        return response.Data.Me.AuthenticateWithPassword switch
        {
            AuthenticationSucceeded success =>
                new CoreApiCredentials(
                    success.AccessToken,
                    success.DeviceId
                ),
            FailedInvalidPassword invalidPassword =>
                throw new InvalidOperationException(
                    invalidPassword.ErrorDetails
                ),
            _ => throw new InvalidOperationException(),
        };
    }

}
