namespace BHBackup.Helpers;

internal static class OfflineUrlHelper
{

    public static string ConvertToOfflineUrl(string relativePath)
    {
        return relativePath.Replace("\\", "/");
    }

    public static string GetShortItemId(string itemId)
    {
        var shortId = itemId.Split('-')[0];
        return (shortId.Length > 8)
            ? shortId[0..8]
            : shortId;
    }

    public static string? GetPaddedIndex(int? index)
    {
        return index?.ToString()?.PadLeft(3, '0');
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

    public static string GetContentFileRelativePath(
        string filename, string subfolderName, DateTime? createdDate, string parentId, string itemId, int? index
    )
    {
        ArgumentNullException.ThrowIfNull(filename);
        ArgumentNullException.ThrowIfNull(subfolderName);
        ArgumentNullException.ThrowIfNull(parentId);
        ArgumentNullException.ThrowIfNull(itemId);
        // "familyapp\\files\\<subfoldername>\\yyyy-mm-dd-<parentItemId>-<index>-<imageId>-<filenamex>.<extension>"
        var relativeFilename = string.Join('-',
            createdDate?.ToString("yyyy-MM-dd"),
            OfflineUrlHelper.GetShortItemId(parentId),
            OfflineUrlHelper.GetPaddedIndex(index),
            OfflineUrlHelper.GetShortItemId(itemId),
            filename
        );
        return Path.Join(
            "familyapp", "files", relativeFilename
        );
    }

    public static string GetContentFileOfflineUrl(
        string filename, string subfolderName, DateTime? createdDate, string parentId, string itemId, int? index
    )
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            OfflineUrlHelper.GetContentFileRelativePath(
                filename, subfolderName, createdDate, parentId, itemId, index
            )
        );
    }

    public static string GetContentImageRelativePath(
        string onlineUrl, string subfolderName, DateTime? createdDate, string parentId, string itemId, int? index)
    {
        ArgumentNullException.ThrowIfNull(onlineUrl);
        ArgumentNullException.ThrowIfNull(subfolderName);
        ArgumentNullException.ThrowIfNull(parentId);
        ArgumentNullException.ThrowIfNull(itemId);
        // "familyapp\\images\\<subfoldername>\\yyyy-mm-dd-<parentItemId>-<index>-<imageId>.jpg"
        var relativeFilename = string.Join('-',
                new[]
                {
                    createdDate?.ToString("yyyy-MM-dd"),
                    OfflineUrlHelper.GetShortItemId(parentId),
                    OfflineUrlHelper.GetPaddedIndex(index),
                    OfflineUrlHelper.GetShortItemId(itemId)
                }.Where(part => !string.IsNullOrEmpty(part))
            ) + Path.GetExtension(new Uri(onlineUrl).AbsolutePath);
        return Path.Join(
            "familyapp", "images", subfolderName, relativeFilename
        ).Replace("\\", "/");
    }

    public static string GetContentImageOfflineUrl(
        string onlineUrl, string subfolderName, DateTime? createdDate, string parentId, string itemId, int? index
    )
    {
        return OfflineUrlHelper.ConvertToOfflineUrl(
            OfflineUrlHelper.GetContentImageRelativePath(
                onlineUrl, subfolderName, createdDate, parentId, itemId, index
            )
        );
    }

}
