using BHDownload.Client.ApiV1.Feeds.Models;
using BHDownload.Client.ApiV2.Models;
using BHDownload.Client.GraphQl.ChildNotes.Models;
using BHDownload.Client.GraphQl.Identity.Models;
using BHDownload.Client.GraphQl.Observations.Models;
using BHDownload.Helpers;
using System.Collections.ObjectModel;

namespace BHDownload.Export;

internal sealed class FamilyAppRepository
{

    public FamilyAppRepository(
        Me identity,
        Sidebar sidebar,
        IEnumerable<ChildSummary> childSummaries,
        IEnumerable<FeedItem> feedItems,
        IEnumerable<Observation> observations,
        IEnumerable<ChildNote> childNotes 
    )
    {
        this.Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        this.Sidebar = sidebar ?? throw new ArgumentNullException(nameof(sidebar));
        this.ChildSummaries = new(
            (childSummaries ?? throw new ArgumentNullException(nameof(childSummaries)))
                .ToList()
        );
        this.FeedItems = new(
            (feedItems ?? throw new ArgumentNullException(nameof(feedItems)))
                .ToList()
        );
        this.Observations = new(
            (observations ?? throw new ArgumentNullException(nameof(observations)))
                .ToList()
        );
        this.ChildNotes = new(
            (childNotes ?? throw new ArgumentNullException(nameof(childNotes)))
                .ToList()
        );
    }

    public Me Identity
    {
        get;
    }

    public Sidebar Sidebar
    {
        get;
    }

    public ReadOnlyCollection<ChildSummary> ChildSummaries
    {
        get;
    }

    public ReadOnlyCollection<FeedItem> FeedItems
    {
        get;
    }

    public ReadOnlyCollection<Observation> Observations
    {
        get;
    }

    public ReadOnlyCollection<ChildNote> ChildNotes
    {
        get;
    }

}
