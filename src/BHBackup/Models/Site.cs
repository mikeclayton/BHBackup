namespace BHBackup.Models;

internal sealed class Site
{

    public Site(NewsfeedPage newsfeedPage, List<ChildNotesPage> childNotesPages)
    {
        this.NewsfeedPage = newsfeedPage ?? throw new ArgumentNullException(nameof(newsfeedPage));
        this.ChildNotesPages = childNotesPages ?? throw new ArgumentNullException(nameof(childNotesPages));
    }

    public NewsfeedPage NewsfeedPage
    {
        get;
    }

    public List<ChildNotesPage> ChildNotesPages
    {
        get;
    }

}
