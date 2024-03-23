namespace BHBackup.Storage.Repositories;

public abstract partial class OfflineRepository<T>
{

    protected OfflineRepository(string rootFolder, bool roundtrip)
    {
        this.RootFolder = rootFolder ?? throw new ArgumentNullException(nameof(rootFolder));
        this.Roundtrip = roundtrip;
    }

    public string RootFolder
    {
        get;
    }

    public bool Roundtrip
    {
        get;
    }

}
