using BHBackup.Client.GraphQl.ChildNotes.Models;

namespace BHBackup.Storage.Repositories;

public sealed class ChildNoteRepository : OfflineRepository<ChildNote>
{

    internal ChildNoteRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetChildNoteFileRootPath()
    {
        return Path.Join(
            "data", "childnotes"
        );
    }

    private static string GetChildNoteFileRelativePath(string childNoteId)
    {
        return Path.Join(
            ChildNoteRepository.GetChildNoteFileRootPath(),
            $"childnote-{childNoteId}.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<ChildNote> ReadAll()
    {
        Console.WriteLine("reading cached child notes...");
        var cacheFiles = base.GetRepositoryFiles(
            ChildNoteRepository.GetChildNoteFileRootPath(),
            "childnote-*.json"
        );
        var childNotes = cacheFiles.Select(
                cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
            ).OrderByDescending(childNote => childNote.CreatedAtParsed);
        return childNotes;
    }

    public override ChildNote ReadItem(string id)
    {
        return base.ReadRepositoryJsonFile(
            ChildNoteRepository.GetChildNoteFileRelativePath(id)
        );
    }

    public override void WriteItem(ChildNote item)
    {
        this.WriteRepositoryJsonFile(
            ChildNoteRepository.GetChildNoteFileRelativePath(item.Id),
            item
        );
    }

    #endregion

}
