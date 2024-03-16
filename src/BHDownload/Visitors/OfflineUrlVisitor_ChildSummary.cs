using BHDownload.Client.ApiV2.Models;
using BHDownload.Helpers;

namespace BHDownload.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(SummaryChild child)
    {
        // child profile - banner image
        child.Image.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
            child.Image.Large, child.Name.FullName
        );
    }

}
