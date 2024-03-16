using BHDownload.Client.Core;

namespace BHDownload.Client.GraphQl;

internal sealed partial class GraphQlClient : CoreApiClient
{

    public GraphQlClient(HttpClient httpClient, Func<CoreApiCredentials> credentialFactory)
        : base(httpClient, credentialFactory)
    {
    }

    private async Task<TResponse> ExecuteGraphQlRequest<TResponse>(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip)
    {

        return await base.ExecuteJsonRequestAsync<TResponse>(
            CoreApiClient.JoinUrl(CoreApiClient.FamilyAppUri, requestUrl), querystring, method, requestBody, roundtrip
        ) ?? throw new InvalidOperationException();
    }

}
