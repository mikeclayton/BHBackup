namespace BHBackup.Models;

internal sealed class ChildProfilePages
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
