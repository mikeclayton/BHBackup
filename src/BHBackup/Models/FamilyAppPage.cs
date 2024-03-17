namespace BHBackup.Models;

internal class FamilyAppPage : GenericPage
{

    public FamilyAppPage(
        string name,
        string templateFilename, string outputFilename,
        string title,
        TopBar topBar
    ) : base(name, templateFilename, outputFilename)
    {
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.TopBar = topBar ?? throw new ArgumentNullException(nameof(topBar));
    }

    public string Title
    {
        get;
    }

    public TopBar TopBar
    {
        get;
    }

}
