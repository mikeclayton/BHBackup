using BHBackup.Storage.Helpers;

namespace BHBackup.Render.Models.Site;

public class GenericPage
{

    public GenericPage(
        string name,
        string templateFilename, string outputFilename
    )
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.TemplateFilename = templateFilename ?? throw new ArgumentNullException(nameof(templateFilename));
        this.OutputFilename = outputFilename ?? throw new ArgumentNullException(nameof(outputFilename));
    }

    public string Name
    {
        get;
    }

    public string TemplateFilename
    {
        get;
    }

    public string OutputFilename
    {
        get;
    }

    /// <summary>
    /// Used by liquid templates to generate relative links between pages.
    /// </summary>
    public string OfflineUrl =>
        OfflineUrlHelper.ConvertToOfflineUrl(
            this.OutputFilename
        );

}
