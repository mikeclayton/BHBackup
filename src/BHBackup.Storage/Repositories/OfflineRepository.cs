using BHBackup.Common.Helpers;
using System.Text.Json;

namespace BHBackup.Storage.Repositories;

public static class OfflineRepository
{

    public static string GetAbsoluteFilename(string rootFolder, string relativePath)
    {
        return Path.Join(rootFolder, relativePath);
    }

    public static string[] GetRepositoryFiles(string rootFolder, string relativePath, string pattern)
    {
        var absolutePath = OfflineRepository.GetAbsoluteFilename(rootFolder, relativePath);
        if (!Directory.Exists(absolutePath))
        {
            throw new InvalidOperationException();
        }
        return Directory.GetFiles(absolutePath, pattern);
    }

    public static T ReadRepositoryJsonFile<T>(string rootFolder, string relativePath, bool roundtrip)
    {
        return OfflineRepository.ReadRepositoryJsonFile<T>(rootFolder, relativePath, false, roundtrip);
    }

    public static T ReadRepositoryJsonFile<T>(string rootFolder, string path, bool isAbsolutePath, bool roundtrip)
    {
        var absolutePath = isAbsolutePath
            ? path
            : OfflineRepository.GetAbsoluteFilename(rootFolder, path);
        if (!File.Exists(absolutePath))
        {
            throw new InvalidOperationException();
        }
        var fileText = File.ReadAllText(absolutePath);
        var fileObject = JsonSerializer.Deserialize<T>(
                fileText
            ) ?? throw new InvalidOperationException();
        if (roundtrip)
        {
            var fileJson = JsonHelper.Prettify(fileText);
            var objectJson = JsonHelper.ConvertToJson(fileObject);
            if (fileJson != objectJson)
            {
                throw new InvalidOperationException();
            }
        }
        return fileObject;
    }

    public static void WriteRepositoryJsonFile<T>(string rootFolder, string path, T item, bool isAbsolutePath = false)
    {
        var absolutePath = isAbsolutePath
            ? path
            : OfflineRepository.GetAbsoluteFilename(rootFolder, path);
        // create the directory if it doesn't already exist
        _ = Directory.CreateDirectory(
             Path.GetDirectoryName(absolutePath) ?? throw new InvalidOperationException()
        );
        // save the item to disk
        File.WriteAllText(
            absolutePath,
            JsonHelper.ConvertToJson(item)
        );
    }

}
