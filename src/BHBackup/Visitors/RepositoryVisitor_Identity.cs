using BHBackup.Client.GraphQl.Identity.Models;

namespace BHBackup.Visitors;

internal abstract partial class RepositoryVisitor
{

    public virtual void Visit(Me identity)
    {
        this.Visit(identity.CurrentContext);
        foreach (var availableContext in identity.AvailableContexts)
        {
            this.Visit(availableContext);
        }
    }

    public virtual void Visit(UserContext context)
    {
        this.Visit(context.Target);
    }

    public virtual void Visit(PersonContextTarget target)
    {
        this.Visit(target.Person);
        foreach (var child in target.Children)
        {
            this.Visit(child);
        }
    }

    public virtual void Visit(Person person)
    {
    }

    public virtual void Visit(Child child)
    {
    }

}
