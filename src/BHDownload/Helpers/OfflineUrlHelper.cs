namespace BHDownload.Helpers;

internal static class OfflineUrlHelper
{

    public static string ConvertToOfflineUrl(string relativePath)
    {
        return relativePath.Replace("\\", "/");
    }

    public static string JoinUrlParts(params string[] parts)
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            Path.Join(parts)
        );
    }

    public static string GetProfileImageRelativePath(string onlineUrl, string profileName)
    {
        var filename = $"profile-{profileName.Replace(" ", "-")}"
            + Path.GetExtension(new Uri(onlineUrl).AbsolutePath);
        return Path.Join("familyapp", "profiles", filename);
    }

    public static string GetProfileImageOfflineUrl(string onlineUrl, string profileName)
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            OfflineUrlHelper.GetProfileImageRelativePath(
                onlineUrl, profileName
            )
        );
    }

    public static string GetContentFileRelativePath(string filename, string subfolderName, DateTime? createdDate, string itemId)
    {
        var relativeFilename = $"{createdDate:yyyy-MM-dd}-{itemId.Split('-')[0]}-{filename}";
        return OfflineUrlHelper.JoinUrlParts(
            "familyapp", "files", relativeFilename
        );
    }

    public static string GetContentFileOfflineUrl(string filename, string subfolderName, DateTime? createdDate, string itemId)
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            OfflineUrlHelper.GetContentFileRelativePath(
                filename, subfolderName, createdDate, itemId
            )
        );
    }

    public static string GetContentImageRelativePath(string onlineUrl, string subfolderName, DateTime? createdDate, string itemId, int? index)
    {
        // "familyapp\\images\\<subfoldername>\\yyyy-mm-dd-<parentItemId>-001.jpg"
        var filename = string.Join("-",
                createdDate?.ToString("yyyy-MM-dd"),
                itemId.Split('-')[0],
                index?.ToString()?.PadLeft(3, '0')
                ) + Path.GetExtension(
                    new Uri(onlineUrl).AbsolutePath
            );
        return Path.Join(
            "familyapp", "images", subfolderName, filename
        ).Replace("\\", "/");
    }

    public static string GetContentImageOfflineUrl(string onlineUrl, string subfolderName, DateTime? createdDate, string itemId, int? index)
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            OfflineUrlHelper.GetContentImageRelativePath(
                onlineUrl, subfolderName, createdDate, itemId, index
            )
        );
    }

}
