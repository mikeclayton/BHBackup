using BHBackup.Client.Core;

namespace BHBackup.Client.GraphQl;

public sealed partial class GraphQlClient : CoreApiClient
{

    public GraphQlClient(HttpClient httpClient, CoreApiCredentials? apiCredentials = null)
        : base(httpClient, apiCredentials)
    {
    }

    public async Task<TResponse> ExecuteGraphQlRequest<TResponse>(
        string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip
    )
    {
        return await base.ExecuteJsonRequestAsync<TResponse>(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody, roundtrip
        ).ConfigureAwait(false);
    }

}
