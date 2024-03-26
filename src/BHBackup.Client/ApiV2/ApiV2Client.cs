using BHBackup.Client.Core;

namespace BHBackup.Client.ApiV2;

public sealed partial class ApiV2Client : CoreApiClient
{

    public ApiV2Client(HttpClient httpClient, CoreApiCredentials? apiCredentials = null)
        : base(httpClient, apiCredentials)
    {
    }

    public async Task<string> ExecuteApiV2Request(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, string? requestBody)
    {

        return await base.ExecuteTextRequestAsync(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody
        ).ConfigureAwait(false) ?? throw new InvalidOperationException();
    }

    public async Task<TResponse> ExecuteApiV2Request<TResponse>(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip)
    {

        return await base.ExecuteJsonRequestAsync<TResponse>(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody, roundtrip
        ).ConfigureAwait(false) ?? throw new InvalidOperationException();
    }

}
