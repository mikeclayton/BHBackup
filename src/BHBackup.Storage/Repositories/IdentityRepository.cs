using BHBackup.Client.GraphQl.Identity.Api;

namespace BHBackup.Storage.Repositories;

public sealed class IdentityRepository : OfflineRepository<GetCurrentContextResponse>
{

    internal IdentityRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetCurrentContextFileRootPath()
    {
        return Path.Join(
            "data", "identity"
        );
    }

    private static string GetCurrentContextFileRelativePath()
    {
        return Path.Join(
            IdentityRepository.GetCurrentContextFileRootPath(),
            "current-context.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<GetCurrentContextResponse> ReadAll()
    {
        Console.WriteLine("reading identity...");
        var cacheFiles = base.GetRepositoryFiles(
            IdentityRepository.GetCurrentContextFileRootPath(),
            "current-context.json"
        );
        var childNotes = cacheFiles.Select(
            cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
        );
        return childNotes;
    }

    public GetCurrentContextResponse ReadItem()
    {
        return base.ReadRepositoryJsonFile(
            IdentityRepository.GetCurrentContextFileRelativePath()
        );
    }

    public override GetCurrentContextResponse ReadItem(string id)
    {
        throw new NotImplementedException();
    }

    public override void WriteItem(GetCurrentContextResponse item)
    {
        this.WriteRepositoryJsonFile(
            IdentityRepository.GetCurrentContextFileRelativePath(),
            item
        );
    }

    #endregion

}
