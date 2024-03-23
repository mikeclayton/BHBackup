using BHBackup.Client.ApiV1.Feeds.Models;

namespace BHBackup.Storage.Visitors;

public abstract partial class RepositoryVisitor
{

    public virtual void Visit(IEnumerable<FeedItem> feedItems)
    {
        foreach (var feedItem in feedItems)
        {
            this.Visit(feedItem);
        }
    }

    public virtual void Visit(FeedItem feedItem)
    {
        this.Visit(feedItem.Sender);
        foreach (var feedFile in feedItem.Files)
        {
            this.Visit(feedFile);
        }
    }

    public virtual void Visit(FeedSender sender)
    {
    }

    public virtual void Visit(FeedFile file)
    {
    }

}
