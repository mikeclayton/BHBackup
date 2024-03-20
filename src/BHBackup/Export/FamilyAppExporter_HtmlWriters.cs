using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Helpers;
using BHBackup.Models;
using Fluid;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text.Encodings.Web;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private void RenderLiquidTemplate(GenericPage page, FamilyAppRepository repository)
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
        options.MemberAccessStrategy.Register<FamilyAppRepository>();
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
        var filteredRepository = new FamilyAppRepository(
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

        var renderedHtml = template.Render(context, HtmlEncoder.Default);

        var outputFilename = this.DownloadHelper.GetAbsoluteFilename(page.OutputFilename);
        Directory.CreateDirectory(
            Path.GetDirectoryName(outputFilename) ?? throw new InvalidOperationException()
        );
        File.WriteAllText(outputFilename, renderedHtml);

    }

}