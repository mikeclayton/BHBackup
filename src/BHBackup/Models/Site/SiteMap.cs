namespace BHBackup.Models;

internal sealed class SiteMap
{

    public SiteMap(NewsfeedPage newsfeedPage, List<ChildProfilePages> childProfiles)
    {
        this.NewsfeedPage = newsfeedPage ?? throw new ArgumentNullException(nameof(newsfeedPage));
        this.ChildProfiles = childProfiles ?? throw new ArgumentNullException(nameof(childProfiles));
    }

    public NewsfeedPage NewsfeedPage
    {
        get;
    }

    public List<ChildProfilePages> ChildProfiles
    {
        get;
    }

}
