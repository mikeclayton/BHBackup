using BHBackup.Client.ApiV2.Sidebar.Models;

namespace BHBackup.Visitors;

internal abstract partial class RepositoryVisitor
{

    public virtual void Visit(Sidebar sidebar)
    {
        this.Visit(sidebar.Items);
    }

    public virtual void Visit(IEnumerable<SidebarItem> items)
    {
        foreach (var item in items)
        {
            this.Visit(item);
        }
    }

    public virtual void Visit(SidebarItem item)
    {
    }

}
