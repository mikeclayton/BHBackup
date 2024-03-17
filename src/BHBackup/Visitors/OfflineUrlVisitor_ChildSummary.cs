using BHBackup.Client.ApiV2.Models;
using BHBackup.Helpers;

namespace BHBackup.Visitors;

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
