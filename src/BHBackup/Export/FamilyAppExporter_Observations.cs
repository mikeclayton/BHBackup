using BHBackup.Client.GraphQl.Observations;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<Observation> DownloadObservationsData(IEnumerable<string> observationIds)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the observations from the api
        Console.WriteLine("downloading observation data...");
        var observations = observationIds
            .Chunk(5)
            .Select(
                chunk => graphQlClient.ObservationsByIds(chunk).Result
            ).SelectMany(
                response => response.Data.ChildDevelopment.Observations.Results
            ).ToList();
        // save the observations to disk in individual files
        foreach (var observation in observations)
        {
            this.WriteRepositoryJsonFile(
                OfflinePathHelper.GetObservationDataFileRelativePath(observation.Id),
                observation
            );
            yield return observation;
        }
    }

    private IEnumerable<Observation> ReadObservationsData(bool roundtrip)
    {
        Console.WriteLine("reading cached observations...");
        var cacheFiles = this.GetRepositoryFiles(
            OfflinePathHelper.GetObservationDataFileRootPath(),
            "observation-*.json"
        );
        // check the files roundtrip to make sure we've got a complete and accurate object model
        var observations = cacheFiles.Select(
                cacheFile => this.ReadRepositoryJsonFile<Observation>(cacheFile, roundtrip, true)
            ).ToList()
            .AsReadOnly();
        return observations;
    }

}