using BHBackup.Client.ApiV2.Sidebar.Models;

namespace BHBackup.Storage.Repositories;

public sealed class SidebarRepository : OfflineRepository<Sidebar>
{

    internal SidebarRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetSidebarFileRootPath()
    {
        return Path.Join(
            "data", "sidebar"
        );
    }

    private static string GetSidebarFileRelativePath()
    {
        return Path.Join(
            SidebarRepository.GetSidebarFileRootPath(),
            "sidebar.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<Sidebar> ReadAll()
    {
        Console.WriteLine("reading identity...");
        var cacheFiles = base.GetRepositoryFiles(
            SidebarRepository.GetSidebarFileRootPath(),
            "sidebar.json"
        );
        var childNotes = cacheFiles.Select(
            cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
        );
        return childNotes;
    }

    public Sidebar ReadItem()
    {
        return base.ReadRepositoryJsonFile(
            SidebarRepository.GetSidebarFileRelativePath()
        );
    }

    public override Sidebar ReadItem(string id)
    {
        throw new NotImplementedException();
    }

    public override void WriteItem(Sidebar item)
    {
        this.WriteRepositoryJsonFile(
            SidebarRepository.GetSidebarFileRelativePath(),
            item
        );
    }

    #endregion

}
