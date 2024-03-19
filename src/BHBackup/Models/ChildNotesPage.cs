using BHBackup.Client.ApiV2.ChildSummary.Models;

namespace BHBackup.Models;

internal sealed class ChildNotesPage : FamilyAppPage
{

    public ChildNotesPage(
        string name,
        string templateFilename, string outputFilename,
        string title,
        TopBar topBar,
        ChildSummary childSummary
    ) : base(name, templateFilename, outputFilename, title, topBar)
    {
        this.ChildSummary = childSummary ?? throw new ArgumentNullException(nameof(childSummary));
    }

    public ChildSummary ChildSummary
    {
        get;
    }

}
