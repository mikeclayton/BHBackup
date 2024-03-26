namespace BHBackup.Client.ApiV2.ChildSummary;

public static class ChildSummaryExtensions
{

    public static async Task<Models.ChildSummary> GetChildSummary(
        this ApiV2Client apiV2Client,
        string childId
    )
    {
        return await apiV2Client.ExecuteApiV2Request<Models.ChildSummary>(
            requestUrl: $"api/v2/children/{childId}/summary",
            querystring: null,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        ).ConfigureAwait(false);
    }

}
