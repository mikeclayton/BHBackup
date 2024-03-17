namespace BHBackup.Models;

internal sealed class TopBar
{

    public TopBar(string selectedIcon, string title)
    {
        this.SelectedIcon = selectedIcon ?? throw new ArgumentNullException(nameof(selectedIcon));
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string SelectedIcon
    {
        get;
    }

    public string Title
    {
        get;
    }

}
