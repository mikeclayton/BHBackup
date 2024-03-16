using BHDownload.Client.ApiV1.Feeds.Api;
using BHDownload.Client.Core;

namespace BHDownload.Client.ApiV1;

internal sealed partial class ApiV1Client : CoreApiClient
{

    public async Task<GetFeedsResponse> GetFeedItems(DateTime? olderThan, int targetHeight = 1331)
    {

        var querystring = new Dictionary<string, string>();
        if (olderThan is not null)
        {
            querystring.Add("olderThan", olderThan.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss+00:00"));
        }
        querystring.Add("targetHeight", targetHeight.ToString());

        return await this.ExecuteApiV1Request<GetFeedsResponse>(
            requestUrl: "api/feed/feed/feed",
            querystring: querystring,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        ) ?? throw new InvalidOperationException();

    }

}
