using BHDownload.Client.Core;
using BHDownload.Client.GraphQl;
using BHDownload.Client.GraphQl.Observations.Models;
using BHDownload.Helpers;

namespace BHDownload.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<Observation> DownloadObservations(IEnumerable<string> observationIds)
    {

        var graphQlClient = new GraphQlClient(
            this.HttpClient,
            () => LoginHelpers.Authenticate(
                this.HttpClient,
                this.Username,
                this.Password,
                this.DeviceId
            ).Result
        );

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

    private IEnumerable<Observation> ReadObservations(bool roundtrip) 
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