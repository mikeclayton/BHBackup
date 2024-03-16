using BHDownload.Client.GraphQl.ChildNotes.Models;
using BHDownload.Helpers;

namespace BHDownload.Visitors;

internal abstract partial class RepositoryVisitor
{

    public virtual void Visit(IEnumerable<ChildNote> childNotes)
    {
        foreach (var childNote in childNotes)
        {
            this.Visit(childNote);
        }
    }

    public virtual void Visit(ChildNote childNote)
    {
        this.Visit(childNote.CreatedBy);
        this.Visit(childNote.ModifiedBy);
    }

    public virtual void Visit(ChildNotesPerson person)
    {
    }

}
