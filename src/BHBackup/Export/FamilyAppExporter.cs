using System.Reflection;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Helpers;
using BHBackup.Models;
using BHBackup.Visitors;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    public FamilyAppExporter(string repositoryDirectory, string username, string password, string deviceId)
    {
        this.RepositoryDirectory = repositoryDirectory ?? throw new ArgumentNullException(nameof(repositoryDirectory));
        this.HttpClient = new HttpClient();
        this.Username = username ?? throw new ArgumentNullException(nameof(username));
        this.Password = password ?? throw new ArgumentNullException(nameof(password));
        this.DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
    }

    public string RepositoryDirectory { get; }

    public HttpClient HttpClient { get; }

    public string Username { get; }

    public string Password { get; }

    public string DeviceId { get; }

    public FamilyAppRepository DownloadRepository()
    {
        var identity = this.DownloadCurrentContext().Data.Me;
        var sidebar = this.DownloadSidebar();
        var summaries = this.DownloadChildSummaries(sidebar).ToBlockingEnumerable().ToList();
        var feedItems = this.DownloadFeedItems().ToList();
        var observations = this.DownloadObservations(
            feedItems
            .Where(feedItem => feedItem.Embed is FeedEmbedObservation)
            .Select(feedItem => ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId)
        ).ToList();
        var childNotes = this.DownloadChildNotes(
            sidebar.ChildProfileItems.Select(child => child.Id)
        ).ToList();
        var repository = new FamilyAppRepository(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    public FamilyAppRepository ReadRepository()
    {
        var roundtrip = true;
        var identity = (this.ReadCurrentContext(roundtrip) ?? throw new InvalidOperationException()).Data.Me;
        var sidebar = this.ReadSidebar(roundtrip) ?? throw new InvalidOperationException();
        var summaries = this.ReadChildSummaries(roundtrip);
        var feedItems = this.ReadFeedItems(roundtrip).ToList();
        var observations = this.ReadObservations(roundtrip).ToList();
        var childNotes = this.ReadChildNotes(roundtrip).ToList();
        var repository = new FamilyAppRepository(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    private static void UpdateOfflineUrls(FamilyAppRepository repository)
    {
        var visitor = new OfflineUrlVisitor();
        visitor.Visit(repository);
    }

    public async Task DownloadResources(FamilyAppRepository repository)
    {
        // static resources
        await this.DownloadStaticHttpFonts(false);
        await this.DownloadStaticHttpImages(false);
        // sidebar
        await this.DownloadSidebarImages(repository.Sidebar, false);
        // profile images
        await this.DownloadProfileImages(repository.FeedItems, false);
        await this.DownloadProfileImages(repository.Observations, false);
        await this.DownloadProfileImages(repository.ChildNotes, false);
        // content images
        await this.DownloadContentImages(repository.FeedItems, false);
        await this.DownloadContentImages(repository.Observations, false);
        await this.DownloadContentImages(repository.ChildNotes, false);
        // files
        await this.DownloadBinaryFiles(repository.FeedItems, false);
    }

    public void GenerateHtmlFiles(FamilyAppRepository repository)
    {

        this.UnpackEmbeddedStylesheets(true);

        var children = repository.Observations
            .SelectMany(observation => observation.Children)
            .DistinctBy(child => child.Id)
            .ToList();

        var index = new GenericPage(
            name: "index",
            templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.index.liquid",
            outputFilename: "index.htm"
        );

        var site = new Site(
            newsfeedPage: new(
                name: "newsfeed",
                templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.newsfeed-00-main-page.liquid",
                outputFilename: OfflinePathHelper.GetNewsfeedPageRelativePath(),
                title: "Bright Horizons | Newsfeed",
                topBar: new(
                    selectedIcon:"home",
                    title:"Newsfeed"
                )
            ),
            childNotesPages: repository.Sidebar.ChildProfileItems
                .Select(
                    item => new ChildNotesPage(
                        name: $"childnotes",
                        templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.childnotes-00-main-page.liquid",
                        outputFilename: OfflinePathHelper.GetChildProfileNotesPageRelativePath(item.Title),
                        title: "Bright Horizons | Child profile",
                        topBar: new(
                            selectedIcon: "home",
                            title: "Child profile"
                        ),
                        childSummary: repository.ChildSummaries.First(childSummary => childSummary.Child.ChildId == item.Id)
                    )
                ).ToList()
        );

        var pages = new List<GenericPage>()
            .Union(new List<GenericPage> { index })
            .Union(new List<GenericPage> { site.NewsfeedPage })
            .Union(site.ChildNotesPages)
            .ToList();
        foreach (var page in pages)
        {
            this.RenderLiquidTemplate(
                site, page, repository
            );
        }

    }
    
}