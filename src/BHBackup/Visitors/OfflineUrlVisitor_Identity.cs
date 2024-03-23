using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Render.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(Person person)
    {
        // profile image
        var profileImage = person?.ProfileImage;
        if (profileImage is null)
        {
            return;
        }
        profileImage.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
            profileImage.Url ?? throw new InvalidOperationException(),
            person?.Name.FullName ?? throw new InvalidOperationException()
        );

    }

    public override void Visit(Child child)
    {
        // profile image
        var profileImage = child?.ProfileImage ?? throw new InvalidOperationException();
        profileImage.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
            child.ProfileImage.Url ?? throw new InvalidOperationException(),
            child?.Name?.FullName ?? throw new InvalidOperationException()
        );
    }

}
