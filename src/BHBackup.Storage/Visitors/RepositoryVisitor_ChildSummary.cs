using BHBackup.Client.ApiV2.ChildSummary.Models;

namespace BHBackup.Storage.Visitors;

public abstract partial class RepositoryVisitor
{

    public virtual void Visit(IEnumerable<ChildSummary> childSummaries)
    {
        foreach (var childSummary in childSummaries)
        {
            this.Visit(childSummary);
        }
    }

    public virtual void Visit(ChildSummary childSummary)
    {
        this.Visit(childSummary.Child);
    }

    public virtual void Visit(SummaryChild summaryChild)
    {
        this.Visit(summaryChild.Image);
    }

    public virtual void Visit(SummaryImage summaryImage)
    {
    }

}
