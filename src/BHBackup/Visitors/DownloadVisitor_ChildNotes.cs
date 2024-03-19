using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models.Context;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor
{

    public override void Visit(IEnumerable<ChildNote> childNotes)
    {
        var childNoteList = childNotes.ToList();
        base.Visit(childNoteList);
        // child notes - profile images
        var profileImages = new List<ProfileImage>()
            .Union(childNoteList.Select(childNote => childNote.CreatedBy.ProfileImage))
            .Union(childNoteList.Select(childNote => childNote.ModifiedBy.ProfileImage))
            .Where(profileImage => profileImage?.Url is not null)
            .Cast<ProfileImage>()
            .DistinctBy(profileImage => profileImage.Url)
            .ToList();
        Console.WriteLine("downloading child note profile images...");
        foreach (var profileImage in profileImages)
        {
            this.DownloadHelper.DownloadHttpResource(
                profileImage.Url, profileImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
        //// child notes - content files
        //Console.WriteLine("downloading child note files");
        //var childNoteFiles = childNotes.SelectMany(
        //    childNote => childNote.Files
        //);
        //foreach (var childNoteFile in childNoteFiles)
        //{
        //    this.DownloadHelper.DownloadHttpResource(
        //        childNoteFile.Url, childNoteFile.OfflineUrl
        //    ).GetAwaiter().GetResult();
        //}
        // observations - content images
        Console.WriteLine("downloading child note content images...");
        var images = childNoteList
            .SelectMany(childNote => childNote.Images)
            .OrderBy(image => image.OfflineUrl)
            .ToList();
        foreach (var childNoteImage in images)
        {
            this.DownloadHelper.DownloadHttpResource(
                childNoteImage.FullSizeUrl, childNoteImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
    }

}
