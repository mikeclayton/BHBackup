using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Storage.Helpers;

namespace BHBackup.Storage.Visitors;

public sealed partial class OfflineUrlVisitor
{

    public override void Visit(ChildNote childNote)
    {
        base.Visit(childNote);
        // child note - content images
        var counter = 1;
        foreach (var image in childNote.Images)
        {
            image.OfflineUrl = OfflineUrlHelper.GetContentImageOfflineUrl(
                image.FullSizeUrl, "childnotes", childNote.CreatedAtParsed, childNote.Id, image.Id, counter
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
