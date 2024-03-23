using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Storage.Helpers;

namespace BHBackup.Storage.Visitors;

public sealed partial class OfflineUrlVisitor
{

    public override void Visit(SummaryChild child)
    {
        // child profile - banner image
        child.Image.OfflineUrl = OfflineUrlHelper.GetProfileImageOfflineUrl(
            child.Image.Large, child.Name.FullName
        );
    }

}
