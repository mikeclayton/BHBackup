namespace BHBackup.Storage.Repositories;

public abstract partial class OfflineRepository<T>
{

    public abstract IEnumerable<T> ReadAll();

    public abstract T ReadItem(string id);

    public abstract void WriteItem(T item);

}
