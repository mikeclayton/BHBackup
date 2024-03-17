using BHBackup.Export;

namespace BHBackup.Visitors;

internal abstract partial class RepositoryVisitor
{

    protected RepositoryVisitor()
    {
    }

    public virtual void Visit(FamilyAppRepository repository)
    {
        this.Visit(repository.Identity);
        this.Visit(repository.Sidebar);
        this.Visit(repository.ChildSummaries);
        this.Visit(repository.FeedItems);
        this.Visit(repository.Observations);
        this.Visit(repository.ChildNotes);
    }

}
