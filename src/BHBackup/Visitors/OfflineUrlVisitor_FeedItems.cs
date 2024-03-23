using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Render.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(FeedItem feedItem)
    {
        base.Visit(feedItem);
        // feed item - profile image
        var sender = feedItem.Sender;
        sender.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
            sender.ProfileImage ?? throw new InvalidOperationException(),
            sender.Name ?? throw new InvalidOperationException()
        );
        // feed item - content files
        var fileCounter = 1;
        foreach (var feedFile in feedItem.Files)
        {
            feedFile.OfflineUrl = OfflineUrlHelper.GetContentFileOfflineUrl(
                feedFile.Filename, "feeditems", feedItem.CreatedDateParsed, feedItem.FeedItemId, feedFile.FileId, fileCounter
            );
            fileCounter++;
        }
        // feed item - content images
        var imageCounter = 1;
        foreach (var feedImage in feedItem.Images)
        {
            feedImage.OfflineUrl = OfflineUrlHelper.GetContentImageOfflineUrl(
                feedImage.UrlBig, "feeditems", feedItem.CreatedDateParsed, feedItem.FeedItemId,  feedImage.ImageId, imageCounter
            );
            imageCounter++;
        }
    }

}
