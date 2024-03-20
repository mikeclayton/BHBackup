using BHBackup.Helpers;
using System.Text.Json;

namespace BHBackup.Storage;

internal abstract partial class OfflineRepository<T>
{

    protected string GetAbsoluteFilename(string relativePath)
    {
        return OfflineRepository.GetAbsoluteFilename(this.RootFolder, relativePath);
    }

    protected string[] GetRepositoryFiles(string relativePath, string pattern)
    {
        return OfflineRepository.GetRepositoryFiles(this.RootFolder, relativePath, pattern);
    }

    protected T ReadRepositoryJsonFile(string relativePath)
    {
        return this.ReadRepositoryJsonFile(relativePath, false);
    }

    protected T ReadRepositoryJsonFile(string path, bool isAbsolutePath)
    {
        var absolutePath = isAbsolutePath
            ? path
            : this.GetAbsoluteFilename(path);
        if (!File.Exists(absolutePath))
        {
            throw new InvalidOperationException();
        }
        var fileText = File.ReadAllText(absolutePath);
        var fileObject = JsonSerializer.Deserialize<T>(
            fileText
        ) ?? throw new InvalidOperationException();
        if (this.Roundtrip)
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

    protected void WriteRepositoryJsonFile(string path, T item, bool isAbsolutePath = false)
    {
        var absolutePath = isAbsolutePath
            ? path
            : this.GetAbsoluteFilename(path);
        // create the directory if it doesn't already exist
        _ = Directory.CreateDirectory(
             Path.GetDirectoryName(absolutePath) ?? throw new InvalidOperationException()
        );
        // save the context to disk
        File.WriteAllText(
            absolutePath,
            JsonHelper.ConvertToJson(item)
        );
    }

}
