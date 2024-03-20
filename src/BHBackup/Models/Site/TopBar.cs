namespace BHBackup.Models;

internal sealed class TopBar
{

    public TopBar(string style, string title)
    {
        this.Style = style ?? throw new ArgumentNullException(nameof(style));
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Style
    {
        get;
    }

    public string Title
    {
        get;
    }

}
