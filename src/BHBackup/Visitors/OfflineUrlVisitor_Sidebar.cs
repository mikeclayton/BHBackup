using BHBackup.Client.ApiV2.Models;
using BHBackup.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(SidebarItem item)
    {
        if (item.Type == SidebarItem.ChildItemType)
        {
            // sidebar - icon
            item.OfflineIcon = OfflineUrlHelper.ConvertToOfflineUrl(
                Path.Join(
                    "familyapp", "profiles",
                    $"profile-sidebar-{item.Id.Split("-")[0]}-{item.Title}".Replace(" ", "-")
                        + Path.GetExtension(new Uri(item.Icon).AbsolutePath)
                )
            );
            // sidebar - link
            item.OfflineLink = OfflineUrlHelper.ConvertToOfflineUrl(
                OfflinePathHelper.GetChildProfileNotesPageRelativePath(item.Title)
            );
        }
    }


}
