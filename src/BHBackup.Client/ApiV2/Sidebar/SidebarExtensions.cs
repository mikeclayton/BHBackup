using BHBackup.Client.ApiV2;
using BHBackup.Client.ApiV2.Sidebar.Models;

public static class SidebarExtensions
{

    public static async Task<Sidebar> GetSidebar(
        this ApiV2Client apiV2Client
    )
    {
        return await apiV2Client.ExecuteApiV2Request<Sidebar>(
            requestUrl: "/api/v2/sidebar",
            querystring: null,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        );
    }

}
