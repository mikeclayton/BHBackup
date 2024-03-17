using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(Observation observation)
    {
        // observation - profile image
        var createdBy = observation.CreatedBy;
        if (createdBy?.ProfileImage is { } profileImage)
        {
            createdBy.ProfileImage.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
                onlineUrl: profileImage?.Url ?? throw new InvalidOperationException(),
                createdBy.Name.FullName ?? throw new InvalidOperationException()
            );
        }
        // observation - content images
        var counter = 1;
        foreach (var image in observation.Images)
        {
            image.OfflineUrl = OfflineUrlHelper.GetContentImageOfflineUrl(
                image.FullSizeUrl, "observations", observation.Remark.DateParsed, observation.Id, image.Id, counter
            );
            counter++;
        }
    }

}
