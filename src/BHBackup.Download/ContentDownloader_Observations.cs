using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Storage.Repositories;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public IEnumerable<Observation> DownloadObservationData(ObservationRepository repository, IEnumerable<string> observationIds)
    {
        var graphQlClient = this.GetGraphQlClient();
        // read the observations from the api
        Console.WriteLine("downloading observation data...");
        var observations = graphQlClient.PaginateObservationsByIds(
                observationIds,
                pageSize: 5,
                onBeforeRequest: () =>
                    Console.WriteLine($"    downloading observation data page...")
            ).ToBlockingEnumerable()
            .SelectMany(response => response.Data.ChildDevelopment.Observations.Results)
            .ToList();
        // save the observations to disk in individual files
        foreach (var observation in observations)
        {
            repository.WriteItem(observation);
            yield return observation;
        }
    }

    public void DownloadObservationContent(IEnumerable<Observation> observations)
    {
        var observationList = observations.ToList();
        // observations - profile images
        Console.WriteLine("downloading observation profile images...");
        var profileImages = observationList
            .Select(observation => observation.CreatedBy.ProfileImage)
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.OfflineUrl)
            .ToList();
        foreach (var profileImage in profileImages)
        {
            this.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
        //// observations - content files
        //Console.WriteLine("downloading feed item files");
        //var observationFiles = observationList.SelectMany(
        //    observation => observation.Files
        //);
        //foreach (var observation in observationFiles)
        //{
        //    this.DownloadHelper.DownloadHttpResource(
        //        observation.Url, observation.OfflineUrl
        //    ).GetAwaiter().GetResult();
        //}
        // observations - content images
        Console.WriteLine("downloading observation content images");
        var observationImages = observationList.SelectMany(
            observation => observation.Images
        );
        foreach (var observationImage in observationImages)
        {
            this.DownloadHttpResource(
                observationImage.FullSizeUrl, observationImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
    }

}
