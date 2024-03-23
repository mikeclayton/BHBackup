using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Common.Helpers;
using BHBackup.Render.Helpers;
using BHBackup.Render.Liquid;
using BHBackup.Render.Liquid.Pages;
using BHBackup.Render.Models.Site;
using BHBackup.Render.Static.Assets;
using BHBackup.Storage;
using Fluid;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text.Encodings.Web;

namespace BHBackup.Render.Export;

public sealed class HtmlWriter
{

    public HtmlWriter(string outputDirectory)
    {
        this.OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
    }

    public string OutputDirectory
    {
        get;
    }


    private void UnpackEmbeddedStylesheets(bool overwrite)
    {

        var assembly = Assembly.GetExecutingAssembly();
        var stylesheets = assembly
            .GetManifestResourceNames()
            .Where(
                name => name.EndsWith(".css")
            ).ToList();

        Console.WriteLine("unpacking embedded stylesheets...");
        var prefix = typeof(StaticAssetResources).Namespace + ".";
        foreach (var stylesheet in stylesheets)
        {

            // work out the target filename
            if (!stylesheet.StartsWith(prefix))
            {
                throw new InvalidOperationException();
            }
            var resourceText = EmbeddedResourceHelper.ReadEmbeddedResourceText(assembly, stylesheet);

            var targetRelativeFilename = OfflinePathHelper.GetAssetResourceFileRelativePath(stylesheet[(prefix.Length)..]);
            var targetAbsoluteFilename = RelativePathMapper.GetAbsolutePath(this.OutputDirectory, targetRelativeFilename);

            if (!overwrite && File.Exists(targetAbsoluteFilename))
            {
                Console.WriteLine($"    skipping '{targetRelativeFilename}'...");
                continue;
            }

            Console.WriteLine($"    unpacking '{targetRelativeFilename}'...");
            Directory.CreateDirectory(
                Path.GetDirectoryName(targetAbsoluteFilename) ?? throw new InvalidOperationException()
            );
            File.WriteAllText(targetAbsoluteFilename, resourceText);

        }

    }

    public void GenerateHtmlFiles(DataCollection repository)
    {

        this.UnpackEmbeddedStylesheets(true);

        var children = repository.Observations
            .SelectMany(observation => observation.Children)
            .DistinctBy(child => child.Id)
            .ToList();

        var index = new GenericPage(
            name: "index",
            templateFilename: $"{typeof(LiquidPageTemplateResources).Namespace}.index.liquid",
            outputFilename: "index.htm"
        );

        var pages = new List<GenericPage>()
            .Concat(
                // index
                new List<GenericPage> { index }
            ).Concat(
                // newsfeed
                new List<GenericPage> {
                    new NewsfeedPage(
                        name: "newsfeed",
                        templateFilename: $"{typeof(LiquidPageTemplateResources).Namespace}.newsfeed-page.liquid",
                        outputFilename: OfflinePathHelper.GetNewsfeedPageRelativePath(),
                        title: "Bright Horizons | Newsfeed",
                        topBar: new(
                            style: "newsfeed",
                            title: "Newsfeed"
                        )
                    )
                }
            )
            //.Concat(
            //    // child profile - journey
            //    repository.Sidebar.ChildProfileItems
            //        .Select(
            //            sidebarItem => new ChildProfilePage(
            //                name: $"childprofile-journey-{sidebarItem.Id}",
            //                templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.childprofile-journey-page.liquid",
            //                outputFilename: OfflinePathHelper.GetChildProfileJourneyPageRelativePath(sidebarItem.Title, sidebarItem.Id),
            //                title: "Bright Horizons | Child profile",
            //                topBar: new(
            //                    style: "child-profile",
            //                    title: "Child profile"
            //                ),
            //                childSummary: repository.ChildSummaries.Single(childSummary => childSummary.Child.ChildId == sidebarItem.Id)
            //            )
            //        )
            //)
            .Concat(
                // child profile - notes
                repository.Sidebar.ChildProfileItems
                    .Select(
                        sidebarItem => new ChildProfilePage(
                            name: $"childprofile-notes-{sidebarItem.Id}",
                            templateFilename: $"{typeof(LiquidPageTemplateResources).Namespace}.childprofile-notes-page.liquid",
                            outputFilename: OfflinePathHelper.GetChildProfileNotesPageRelativePath(sidebarItem.Title, sidebarItem.Id),
                            title: "Bright Horizons | Child profile",
                            topBar: new(
                                style: "child-profile",
                                title: "Child profile"
                            ),
                            childSummary: repository.ChildSummaries.Single(childSummary => childSummary.Child.ChildId == sidebarItem.Id)
                        )
                    )
            )
            .ToList();

        foreach (var page in pages)
        {
            var renderedHtml = this.RenderLiquidTemplate(page, repository);
            var outputFilename = RelativePathMapper.GetAbsolutePath(this.OutputDirectory, page.OutputFilename);
            Directory.CreateDirectory(
                Path.GetDirectoryName(outputFilename) ?? throw new InvalidOperationException()
            );
            File.WriteAllText(outputFilename, renderedHtml);
        }

    }

    public string RenderLiquidTemplate(GenericPage page, DataCollection repository)
    {

        Console.WriteLine($"    writing html file '{page.OutputFilename}'...");

        var executingAssembly = Assembly.GetExecutingAssembly();
        var templateText = EmbeddedResourceHelper.ReadEmbeddedResourceText(
            executingAssembly, page.TemplateFilename
        );

        // https://github.com/sebastienros/fluid#using-fluid-in-your-project
        var parser = new FluidParser();
        if (!parser.TryParse(templateText, out var template, out var error))
        {
            throw new InvalidOperationException(error);
        }

        // https://github.com/sebastienros/fluid#allow-listing-a-specific-type
        var options = new TemplateOptions();

        // scaffold
        options.MemberAccessStrategy.Register<DataCollection>();
        options.MemberAccessStrategy.Register<GenericPage>();
        options.MemberAccessStrategy.Register<FamilyAppPage>();
        options.MemberAccessStrategy.Register<NewsfeedPage>();
        options.MemberAccessStrategy.Register<ChildProfilePage>();
        options.MemberAccessStrategy.Register<TopBar>();
        options.MemberAccessStrategy.Register<Sidebar>();
        options.MemberAccessStrategy.Register<SidebarBehavior>();
        options.MemberAccessStrategy.Register<SidebarItem>();

        // identity
        options.MemberAccessStrategy.Register<Me>();
        options.MemberAccessStrategy.Register<Name>();
        options.MemberAccessStrategy.Register<Person>();
        options.MemberAccessStrategy.Register<PersonContextTarget>();
        options.MemberAccessStrategy.Register<ProfileImage>();
        options.MemberAccessStrategy.Register<UserContext>();

        // child summaries
        options.MemberAccessStrategy.Register<ChildSummary>();
        options.MemberAccessStrategy.Register<SummaryInstitution>();
        options.MemberAccessStrategy.Register<SummaryChild>();
        options.MemberAccessStrategy.Register<SummaryImage>();
        options.MemberAccessStrategy.Register<SummaryChildName>();
        options.MemberAccessStrategy.Register<SummaryGroup>();

        // feed items
        options.MemberAccessStrategy.Register<FeedItem>();
        options.MemberAccessStrategy.Register<FeedSender>();
        options.MemberAccessStrategy.Register<FeedImage>();
        options.MemberAccessStrategy.Register<FeedFile>();
        options.MemberAccessStrategy.Register<FeedEmbed>();
        options.MemberAccessStrategy.Register<FeedEmbedDaycareEvent>();
        options.MemberAccessStrategy.Register<FeedEmbedObservation>();

        // observations
        options.MemberAccessStrategy.Register<Observation>();
        options.MemberAccessStrategy.Register<PublicChild>();
        options.MemberAccessStrategy.Register<ObservationStatus>();
        options.MemberAccessStrategy.Register<ObservationPerson>();
        options.MemberAccessStrategy.Register<ChildDevelopmentAreaRefinement>();
        options.MemberAccessStrategy.Register<ChildDevelopmentObservationRemark>();
        options.MemberAccessStrategy.Register<ChildDevelopmentNextStepRemark>();
        options.MemberAccessStrategy.Register<Area>();
        options.MemberAccessStrategy.Register<ChildDevelopmentAreaRefinementSetting>();
        options.MemberAccessStrategy.Register<AgeBandSetting>();
        options.MemberAccessStrategy.Register<AssessmentOptionSetting>();
        options.MemberAccessStrategy.Register<Framework>();
        options.MemberAccessStrategy.Register<Image>();
        options.MemberAccessStrategy.Register<ImageSecret>();

        // child notes
        options.MemberAccessStrategy.Register<ChildNote>();
        options.MemberAccessStrategy.Register<ChildNotesChild>();
        options.MemberAccessStrategy.Register<ChildNotesImage>();
        options.MemberAccessStrategy.Register<ChildNotesPerson>();

        // read "include" files from embedded resources
        options.FileProvider = new ManifestEmbeddedFileProvider(
            executingAssembly
        );


        // make a new filtered repository with just the records we want in it
        var assessments = repository.FeedItems.Where(
            feedItem =>
                (feedItem.Embed is not null)
                && repository.Observations.Any(
                    observation =>
                        (observation.Id == ((feedItem.Embed as FeedEmbedObservation)?.ObservationId))
                        && (observation.Variant == "ASSESSMENT")
                )
        );
        var daycare = repository.FeedItems.Where(
            feedItem => feedItem.Embed is FeedEmbedDaycareEvent
        );
        var files = repository.FeedItems.Where(
            feedItem => feedItem.Files.Count > 0
        );
        var filteredRepository = new DataCollection(
            identity: repository.Identity,
            sidebar: repository.Sidebar,
            childSummaries: repository.ChildSummaries,
            feedItems: new[]
            {
                repository.FeedItems, assessments, daycare, files
            }[0],
            observations: repository.Observations,
            childNotes: repository.ChildNotes
        );

        var context = new TemplateContext(
            new
            {
                repository = filteredRepository,
                page = page,
            },
            options
        );

        return template.Render(context, HtmlEncoder.Default);

    }

}
