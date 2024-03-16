using BHDownload.Client.ApiV2.Models;
using BHDownload.Client.Core;

namespace BHDownload.Client.ApiV2;

internal sealed partial class ApiV2Client : CoreApiClient
{

    public async Task<ChildSummary> GetChildSummary(string childId)
    {
        return await this.ExecuteApiV2Request<ChildSummary>(
            requestUrl: $"api/v2/children/{childId}/summary",
            querystring: null,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        );
    }

    public async Task<Sidebar> GetSidebar()
    {
        return await this.ExecuteApiV2Request<Sidebar>(
            requestUrl: "/api/v2/sidebar",
            querystring: null,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        );
    }

}
