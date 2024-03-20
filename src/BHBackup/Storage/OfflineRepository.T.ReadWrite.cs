namespace BHBackup.Storage;

internal abstract partial class OfflineRepository<T>
{

    public abstract IEnumerable<T> ReadAll();

    public abstract T ReadItem(string id);

    public abstract void WriteItem(T item);

}
