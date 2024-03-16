using BHDownload.Helpers;
using System.Text.Json;

namespace BHDownload.Export;

internal sealed partial class FamilyAppExporter
{

    public string GetAbsoluteFilename(string relativePath)
    {
        return Path.Join(this.RepositoryDirectory, relativePath);
    }

    public string[] GetRepositoryFiles(string relativePath, string pattern)
    {
        var absolutePath = this.GetAbsoluteFilename(relativePath);
        if (!Directory.Exists(absolutePath))
        {
            throw new InvalidOperationException();
        }
        return Directory.GetFiles(absolutePath, pattern);
    }

    public T ReadRepositoryJsonFile<T>(string relativePath, bool roundtrip = false, bool isAbsolutePath = false)
    {
        var absolutePath = isAbsolutePath
            ? relativePath
            : this.GetAbsoluteFilename(relativePath);
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

    public void WriteRepositoryJsonFile<T>(string path, T value, bool isAbsolutePath = false)
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
            JsonHelper.ConvertToJson(value)
        );
    }

}