﻿namespace BHBackup.Render.Models.Site;

public sealed class NewsfeedPage : FamilyAppPage
{

    public NewsfeedPage(
        string name,
        string templateFilename, string outputFilename,
        string title,
        TopBar topBar
    ) : base(name, templateFilename, outputFilename, title, topBar)
    {
    }

}
