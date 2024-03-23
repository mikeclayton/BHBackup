namespace BHBackup.Common.Helpers;

public static class RelativePathMapper
{

    public static string GetAbsolutePath(string rootDirectory, string relativePath)
    {
        ArgumentNullException.ThrowIfNull(rootDirectory);
        ArgumentNullException.ThrowIfNull(relativePath);
        return Path.Join(rootDirectory, relativePath);
    }

}
