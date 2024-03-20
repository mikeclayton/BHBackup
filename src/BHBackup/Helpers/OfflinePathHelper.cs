namespace BHBackup.Helpers;

internal sealed class OfflinePathHelper
{


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

    public static string GetChildProfileNotesPageRelativePath(string childGivenName, string childId)
    {
        return Path.Join(
            "familyapp", "pages", $"childprofile-{childGivenName}-{OfflineUrlHelper.GetShortItemId(childId)}-notes.htm"
        );
    }

    public static string GetChildProfileJourneyPageRelativePath(string childGivenName, string childId)
    {
        return Path.Join(
            "familyapp", "pages", $"childprofile-{childGivenName}-{OfflineUrlHelper.GetShortItemId(childId)}-journey.htm"
        );
    }

}
