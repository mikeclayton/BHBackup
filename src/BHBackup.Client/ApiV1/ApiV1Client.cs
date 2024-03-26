using BHBackup.Client.Core;

namespace BHBackup.Client.ApiV1;

public sealed partial class ApiV1Client : CoreApiClient
{

    public ApiV1Client(HttpClient httpClient, CoreApiCredentials? apiCredentials = null)
        : base(httpClient, apiCredentials)
    {
    }

    public async Task<TResponse> ExecuteApiV1Request<TResponse>(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip)
    {

        return await base.ExecuteJsonRequestAsync<TResponse>(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody, roundtrip
        ).ConfigureAwait(false) ?? throw new InvalidOperationException();
    }

}
