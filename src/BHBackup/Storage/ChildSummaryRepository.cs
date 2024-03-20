using BHBackup.Client.ApiV2.ChildSummary.Models;

namespace BHBackup.Storage;

internal sealed class ChildSummaryRepository : OfflineRepository<ChildSummary>
{

    public ChildSummaryRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetChildSummaryFileRootPath()
    {
        return Path.Join(
            "data", "summaries"
        );
    }

    private static string GetChildSummaryFileRelativePath(string childId)
    {
        return Path.Join(
            ChildSummaryRepository.GetChildSummaryFileRootPath(),
            $"childsummary-{childId}.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<ChildSummary> ReadAll()
    {
        Console.WriteLine("reading cached child summaries...");
        var cacheFiles = base.GetRepositoryFiles(
            ChildSummaryRepository.GetChildSummaryFileRootPath(),
            "childsummary-*.json"
        );
        return cacheFiles.Select(
            cacheFile => this.ReadRepositoryJsonFile(cacheFile, true)
        );
    }

    public override ChildSummary ReadItem(string id)
    {
        return base.ReadRepositoryJsonFile(
            ChildSummaryRepository.GetChildSummaryFileRelativePath(id)
        );
    }

    public override void WriteItem(ChildSummary item)
    {
        this.WriteRepositoryJsonFile(
            ChildSummaryRepository.GetChildSummaryFileRelativePath(item.Child.ChildId),
            item
        );
    }

    #endregion

}
