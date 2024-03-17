using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models;
using BHBackup.Client.GraphQl.Observations.Models;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private async Task DownloadProfileImages(
        IEnumerable<FeedItem> feedItems,
        bool overwrite
    )
    {
        var profileImages = feedItems
            .Select(feedItem => feedItem.Sender)
            .DistinctBy(sender => sender.ProfileImage)
            .ToList();
        Console.WriteLine("downloading feed item profile images...");
        foreach (var profileImage in profileImages)
        {
            await this.DownloadHttpResource(
                profileImage.ProfileImage, profileImage.OfflineUrl, overwrite
            );
        }
    }

    private async Task DownloadProfileImages(
        IEnumerable<Observation> observations,
        bool overwrite
    )
    {
        var profileImages = observations
            .Select(observation => observation.CreatedBy.ProfileImage)
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.Url)
            .ToList();
        Console.WriteLine("downloading observation profile images...");
        foreach (var profileImage in profileImages)
        {
            await this.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl, overwrite: overwrite
            );
        }
    }

    private async Task DownloadProfileImages(
        IList<ChildNote> childNotes,
        bool overwrite
    )
    {
        var profileImages = new List<ProfileImage>()
            .Union(childNotes.Select(childNote => childNote.CreatedBy.ProfileImage))
            .Union(childNotes.Select(childNote => childNote.ModifiedBy.ProfileImage))
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.Url)
            .ToList();
        Console.WriteLine("downloading child note profile images...");
        foreach (var profileImage in profileImages)
        {
            await this.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl, overwrite: overwrite
            );
        }
    }

}