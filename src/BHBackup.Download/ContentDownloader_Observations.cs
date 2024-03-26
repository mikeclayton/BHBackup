using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Download.Extensions;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async IAsyncEnumerable<Observation> DownloadObservationData(ObservationRepository repository, IEnumerable<string> observationIds)
    {
        var graphQlClient = this.GetGraphQlClient();
        // read the observations from the api
        this.Logger.LogInformation("downloading observation data...");
        var pageIndex = 1;
        var responses = await graphQlClient.PaginateObservationsByIds(
                observationIds,
                pageSize: 5,
                onBeforeRequest: () =>
                {
                    this.Logger.LogInformation($"downloading observation data page {pageIndex}...");
                    pageIndex++;
                }
            ).ConfigureAwait(false).ToListAsync();
        var observations = responses
            .SelectMany(
                response => response.Data.ChildDevelopment.Observations.Results
            ).ToList();
        // save the observations to disk in individual files
        foreach (var observation in observations)
        {
            repository.WriteItem(observation);
            yield return observation;
        }
    }

    public async Task DownloadObservationContent(IEnumerable<Observation> observations)
    {
        var observationList = observations.ToList();
        // observations - profile images
        this.Logger.LogInformation("downloading observation profile images...");
        var profileImages = observationList
            .Select(observation => observation.CreatedBy.ProfileImage)
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.OfflineUrl)
            .ToList();
        foreach (var profileImage in profileImages)
        {
            await this.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl
            ).ConfigureAwait(false);
        }
        //// observations - content files
        //this.Logger.LogInformation("downloading feed item files");
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
        this.Logger.LogInformation("downloading observation content images");
        var observationImages = observationList.SelectMany(
            observation => observation.Images
        );
        foreach (var observationImage in observationImages)
        {
            await this.DownloadHttpResource(
                observationImage.FullSizeUrl, observationImage.OfflineUrl
            ).ConfigureAwait(false);
        }
    }

}
