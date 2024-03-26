using BHBackup.Client.GraphQl.ChildNotes;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Download.Extensions;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async IAsyncEnumerable<ChildNote> DownloadChildNoteData(ChildNoteRepository repository, IEnumerable<string> childIds)
    {
        var graphQlClient = this.GetGraphQlClient();
        // read the child notes from the api
        this.Logger.LogInformation("downloading child notes data...");
        var pageIndex = 1;
        var responses = await graphQlClient.PaginateChildNotes(
                childId: childIds.First(),
                noteTypes: ["Classic"],
                limit: 10,
                parentVisible: true,
                safeguardingConcerns: false,
                sensitive: false,
                onBeforeRequest: () =>
                {
                    this.Logger.LogInformation($"downloading child notes data page {pageIndex}...");
                    pageIndex++;
                }
            ).ConfigureAwait(false).ToListAsync();
        var childNotes = responses
            .SelectMany(
                response => response.Data.ChildNotes.Result
            ).ToList();
        // save the child notes to disk in individual files
        foreach (var childNote in childNotes)
        {
            repository.WriteItem(childNote);
            yield return childNote;
        }
    }

    public async Task DownloadChildNoteContent(IEnumerable<ChildNote> childNotes)
    {
        var childNoteList = childNotes.ToList();
        // child notes - profile images
        var profileImages = new List<ProfileImage>()
            .Union(childNoteList.Select(childNote => childNote.CreatedBy.ProfileImage))
            .Union(childNoteList.Select(childNote => childNote.ModifiedBy.ProfileImage))
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.Url)
            .ToList();
        this.Logger.LogInformation("downloading child note profile images...");
        foreach (var profileImage in profileImages)
        {
            await this.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl
            ).ConfigureAwait(false);
        }
        //// child notes - content files
        //this.Logger.LogInformation("downloading child note files");
        //var childNoteFiles = childNotes.SelectMany(
        //    childNote => childNote.Files
        //);
        //foreach (var childNoteFile in childNoteFiles)
        //{
        //    this.DownloadHelper.DownloadHttpResource(
        //        childNoteFile.Url, childNoteFile.OfflineUrl
        //    ).GetAwaiter().GetResult();
        //}
        // child notes - content images
        this.Logger.LogInformation("downloading child note content images...");
        var images = childNoteList
            .SelectMany(childNote => childNote.Images)
            .OrderBy(image => image.OfflineUrl)
            .ToList();
        foreach (var childNoteImage in images)
        {
            await this.DownloadHttpResource(
                childNoteImage.FullSizeUrl, childNoteImage.OfflineUrl
            ).ConfigureAwait(false);
        }
    }

}
