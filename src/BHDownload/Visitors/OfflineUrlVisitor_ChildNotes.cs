using BHDownload.Client.GraphQl.ChildNotes.Models;
using BHDownload.Helpers;

namespace BHDownload.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(ChildNote childNote)
    {
        base.Visit(childNote);
        // child note - content images
        var counter = 1;
        foreach (var image in childNote.Images)
        {
            image.Secret.OfflineUrl = OfflineUrlHelper.GetContentImageOfflineUrl(
                image.Secret.SourceUrl, "childnotes", childNote.CreatedAtParsed, childNote.Id, counter
            );
            counter++;
        }
    }

    public override void Visit(ChildNotesPerson person)
    {
        // child note - profile image
        if (person?.ProfileImage is { } profileImage)
        {
            person.ProfileImage.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
                onlineUrl: profileImage?.Url ?? throw new InvalidOperationException(),
                person.Name.FullName ?? throw new InvalidOperationException()
            );
        }
    }

}
