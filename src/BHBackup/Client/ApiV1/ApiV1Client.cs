using BHBackup.Client.Core;

namespace BHBackup.Client.ApiV1;

internal sealed partial class ApiV1Client : CoreApiClient
{

    public ApiV1Client(HttpClient httpClient, Func<CoreApiCredentials> credentialFactory)
        : base(httpClient, credentialFactory)
    {
    }

    private async Task<TResponse> ExecuteApiV1Request<TResponse>(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip)
    {

        return await base.ExecuteJsonRequestAsync<TResponse>(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody, roundtrip
        ) ?? throw new InvalidOperationException();
    }

}
