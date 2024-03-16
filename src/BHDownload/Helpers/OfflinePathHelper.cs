namespace BHDownload.Helpers;

internal class OfflinePathHelper
{

    public static string GetChildNotesDataFileRootPath()
    {
        return Path.Join(
            "data", "childnotes"
        );
    }

    public static string GetChildNotesDataFileRelativePath(string childNoteId)
    {
        return Path.Join(
            OfflinePathHelper.GetChildNotesDataFileRootPath(),
            $"childnote-{childNoteId}.json"
        );
    }

    public static string GetFeedItemDataFileRootPath()
    {
        return Path.Join(
            "data", "feeditems"
        );
    }

    public static string GetFeedItemDataFileRelativePath(string feedItemId)
    {
        return Path.Join(
            OfflinePathHelper.GetFeedItemDataFileRootPath(),
            $"feeditem-{feedItemId}.json"
        );
    }


    public static string GetIdentityDataFileRootPath()
    {
        return Path.Join(
            "data", "identity"
        );
    }

    public static string GetCurrentContextDataFileRelativePath()
    {
        return Path.Join(
            OfflinePathHelper.GetIdentityDataFileRootPath(),
            "current-context.json"
        );
    }

    public static string GetChildSummaryDataFileRootPath()
    {
        return Path.Join(
            "data", "summaries"
        );
    }

    public static string GetChildSummaryDataFileRelativePath(string childId)
    {
        return Path.Join(
            OfflinePathHelper.GetChildSummaryDataFileRootPath(),
            $"childsummary-{childId}.json"
        );
    }

    public static string GetObservationDataFileRootPath()
    {
        return Path.Join(
            "data", "observations"
        );
    }

    public static string GetObservationDataFileRelativePath(string observationId)
    {
        return Path.Join(
            OfflinePathHelper.GetObservationDataFileRootPath(),
            $"observation-{observationId}.json"
        );
    }

    public static string GetSidebarDataFileRootPath()
    {
        return Path.Join(
            "data", "sidebar"
        );
    }

    public static string GetSidebarDataFileRelativePath()
    {
        return Path.Join(
            OfflinePathHelper.GetSidebarDataFileRootPath(),
            "sidebar.json"
        );
    }

    public static string GetAssetResourceFileRelativePath(string filename)
    {
        return Path.Join(
            "familyapp", "assets", filename
        );
    }

    public static string GetNewsfeedPageRelativePath()
    {
        return Path.Join(
            "familyapp", "pages", "newsfeed.htm"
        );
    }

    public static string GetChildProfileNotesPageRelativePath(string givenName)
    {
        return Path.Join(
            "familyapp", "pages", $"childprofile-{givenName}-notes.htm"
        );
    }

}
