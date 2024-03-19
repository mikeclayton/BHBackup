using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class OfflineUrlVisitor
{

    public override void Visit(Sidebar sidebar)
    {
        // sidebar items
        var itemCounter = 1;
        foreach (var item in sidebar.Items)
        {
            if (item.Type != SidebarItem.ChildItemType)
            {
                continue;
            }
            // sidebar - icon
            var relativeFilename = string.Join('-',
                    new[]
                    {
                        "profile", "sidebar",
                        OfflineUrlHelper.GetPaddedIndex(itemCounter),
                        item.Title.Replace(" ", "-")
                    }.Where(part => !string.IsNullOrEmpty(part))
                ) + Path.GetExtension(new Uri(item.Icon).AbsolutePath);
            item.OfflineIcon = OfflineUrlHelper.ConvertToOfflineUrl(
                Path.Join(
                    "familyapp", "profiles", relativeFilename
                )
            );
            // sidebar - link
            item.OfflineLink = OfflineUrlHelper.ConvertToOfflineUrl(
                OfflinePathHelper.GetChildProfileNotesPageRelativePath(item.Title)
            );
            itemCounter++;
        }
    }


}
