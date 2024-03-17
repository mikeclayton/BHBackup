﻿using BHBackup.Helpers;
using System.Text.Json;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    public string[] GetRepositoryFiles(string relativePath, string pattern)
    {
        var absolutePath = this.DownloadHelper.GetAbsoluteFilename(relativePath);
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
            : this.DownloadHelper.GetAbsoluteFilename(relativePath);
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
            : this.DownloadHelper.GetAbsoluteFilename(path);
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