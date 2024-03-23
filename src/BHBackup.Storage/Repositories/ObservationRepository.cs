using BHBackup.Client.GraphQl.Observations.Models;

namespace BHBackup.Storage.Repositories;

public sealed class ObservationRepository : OfflineRepository<Observation>
{

    internal ObservationRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetObservationaFileRootPath()
    {
        return Path.Join(
            "data", "observations"
        );
    }

    private static string GetObservationFileRelativePath(string observationId)
    {
        return Path.Join(
            ObservationRepository.GetObservationaFileRootPath(),
            $"observation-{observationId}.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<Observation> ReadAll()
    {
        Console.WriteLine("reading cached observations...");
        var cacheFiles = base.GetRepositoryFiles(
            ObservationRepository.GetObservationaFileRootPath(),
            "observation-*.json"
        );
        var observations = cacheFiles.Select(
                cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
            ).ToList()
            .AsReadOnly();
        return observations;
    }

    public override Observation ReadItem(string id)
    {
        return base.ReadRepositoryJsonFile(
            ObservationRepository.GetObservationFileRelativePath(id)
        );
    }

    public override void WriteItem(Observation item)
    {
        this.WriteRepositoryJsonFile(
            ObservationRepository.GetObservationFileRelativePath(item.Id),
            item
        );
    }

    #endregion

}
