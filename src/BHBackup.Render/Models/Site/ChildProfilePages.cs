namespace BHBackup.Render.Models.Site;

public sealed class ChildProfilePages
{

    public ChildProfilePages(ChildProfilePage? notes, ChildProfilePage journey)
    {
        this.Notes = notes;
        this.Journey= journey;
    }

    public ChildProfilePage? Notes
    {
        get;
    }

    public ChildProfilePage? Journey
    {
        get;
    }

}
