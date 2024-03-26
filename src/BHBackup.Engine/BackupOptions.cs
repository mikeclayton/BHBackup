using Microsoft.Extensions.Logging;

namespace BHBackup.Engine;

public sealed class BackupOptions
{

    public ILogger Logger
    {
        get;
        set;
    }

    public string? Username
    {
        get;
        set;
    }

    public string? Password
    {
        get;
        set;
    }

    public string? OutputDirectory
    {
        get;
        set;
    }

    /// <summary>
    /// Skip downloading and new data and just use data that is already in the output directory.
    /// </summary>
    public bool SkipDownload
    {
        get;
        set;
    }

    /// <summary>
    /// Skip generating the offline website (implicitly enables SkipLaunch when true).
    /// </summary>
    public bool SkipGenerate
    {
        get;
        set;
    }

    /// <summary>
    /// Skip launching the offline website at the end of the backup process.
    /// </summary>
    public bool SkipLaunch
    {
        get;
        set;
    }

    public bool Roundtrip
    {
        get;
        set;
    }

}
