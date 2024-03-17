using BHBackup.Client.GraphQl.ChildNotes;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Helpers;
using System.Collections.ObjectModel;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<ChildNote> DownloadChildNoteData(IEnumerable<string> childIds)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the child notes from the api
        Console.WriteLine("downloading child notes data...");
        var childNotes = graphQlClient.PaginateChildNotes(
                childId: childIds.First(),
                noteTypes: new List<string> { "Classic" },
                limit: 10,
                parentVisible: true,
                safeguardingConcerns: false,
                sensitive: false,
                onBeforeReadPage: () =>
                    Console.WriteLine("    downloading child notes data page...")
            ).ToBlockingEnumerable()
            .SelectMany(
                response => response.Data.ChildNotes.Result
            ).ToList();
        // save the child notes to disk in individual files
        foreach (var childNote in childNotes)
        {
            this.WriteRepositoryJsonFile(
                OfflinePathHelper.GetChildNotesDataFileRelativePath(childNote.Id),
                childNote
            );
            yield return childNote;
        }
        ;    }

    private ReadOnlyCollection<ChildNote> ReadChildNoteData(bool roundtrip)
    {
        Console.WriteLine("reading cached child notes...");
        var cacheFiles = this.GetRepositoryFiles(
            OfflinePathHelper.GetChildNotesDataFileRootPath(),
            "childnote-*.json"
        );
        // check the files roundtrip to make sure we've got a complete and accurate object model
        var childNotes = cacheFiles.Select(
               cacheFile => this.ReadRepositoryJsonFile<ChildNote>(cacheFile, roundtrip, true)
            ).OrderByDescending(childNote => childNote.CreatedAtParsed)
            .ToList()
            .AsReadOnly();
        return childNotes;
    }

}