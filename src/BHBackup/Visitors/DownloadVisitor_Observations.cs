using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations.Models;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor
{

    public override void Visit(IEnumerable<Observation> observations)
    {
        var observationList = observations.ToList();
        base.Visit(observationList);
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
            this.DownloadHelper.DownloadHttpResource(
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
            this.DownloadHelper.DownloadHttpResource(
                observationImage.FullSizeUrl, observationImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
    }

}
