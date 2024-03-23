using BHBackup.Client.Core;
using BHBackup.Download;
using BHBackup.Render.Export;
using BHBackup.Storage;
using BHBackup.Storage.Visitors;
using CommandLine;
using System.Diagnostics;
using System.Reflection;
using static Crayon.Output;

namespace BHBackup;

internal sealed class CmdLineArgs
{

    [Option('?', "help", Required = false, HelpText = "More information about this utility.")]
    public bool Help
    {
        get;
        set;
    }

    [Option('u', "username", Required = false, HelpText = "Your username for 'https://familyapp.brighthorizons.co.uk' website.")]
    public string? Username
    {
        get;
        set;
    }

    [Option('p', "username", Required = false, HelpText = "Your password for 'https://familyapp.brighthorizons.co.uk' website.")]
    public string? Password
    {
        get;
        set;
    }

    [Option('o', "output-directory", Required = false, HelpText = "Name of the folder to download the site to.")]
    public string? OutputDirectory
    {
        get;
        set;
    }

    [Option('w', "skip-download", Required = false)]
    public bool SkipDownload
    {
        get;
        set;
    }

    [Option('g', "skip-generate", Required = false)]
    public bool SkipGenerate
    {
        get;
        set;
    }

    public static void Main(string[] args)
    {
        var parser = new Parser(config => config.HelpWriter = null);
        parser.ParseArguments<CmdLineArgs>(args)
            .WithParsed(
                cmdLineArgs => {
                    if (cmdLineArgs.Help)
                    {
                        CmdLineArgs.DoHelp();
                    }
                    else
                    {
                        CmdLineArgs.DoDownload(cmdLineArgs).GetAwaiter().GetResult();
                    }
                }
        );
    }

    private static void DoBanner()
    {
        var original = new List<string>
        {
            @"-----------------------------------------------------------------------",
            @"-----------------------------------------------------------------------",
            @"               ____  __  ______             __                         ",
            @"              / __ )/ / / / __ )____ ______/ /____  ______             ",
            @"             / __  / /_/ / __  / __ `/ ___/ //_/ / / / __ \            ",
            @"            / /_/ / __  / /_/ / /_/ / /__/ ,< / /_/ / /_/ /            ",
            @"           /_____/_/ /_/_____/\__,_/\___/_/|_|\__,_/ .___/             ",
            @"                                                  /_/                  ",
            @"-----------------------------------------------------------------------",
            @"-----------------------------------------------------------------------",
            @"                                                                       ",
            @" BHBackup v0.1.0.0 - Bright Horizons FamilyApp website backup utility  ",
            @"                                                                       ",
            @" Copyright (c) 2024 Michael Clayton                                    ",
            @" https://github.com/mikeclayton/BHBackup                               ",
            @"                                                                       ",
            @" This program lets you download and save the notes and images from     ",
            @" your child's account on the Bright Horizons ""FamilyApp"" website.    ",
            @"                                                                       ",
            @" All trademarks, logos and brand names are the property of their       ",
            @" respective owners. The makers of this program are not affiliated      ",
            @" with Bright Horizons or Famly. Licensed for use under the terms       ",
            @" of The MIT License. Use this program at your own risk.                "
        };

        var assembly = Assembly.GetExecutingAssembly();
        var assemblyInfo = (
            Product: assembly.GetCustomAttributes<AssemblyProductAttribute>().Single().Product,
            Title: assembly.GetCustomAttributes<AssemblyTitleAttribute>().Single().Title,
            Copyright: assembly.GetCustomAttributes<AssemblyCopyrightAttribute>().Single().Copyright,
            Description: assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().Single().Description
                .Split("\r\n").Select(line => line.Trim()).Where(line => line.Length > 0).ToList(),
            Version: assembly.GetName().Version,
            Disclaimer: (assembly.GetCustomAttributes<AssemblyMetadataAttribute>().Single(attr => attr.Key == "Disclaimer").Value
                ?? throw new InvalidOperationException())
                    .Split("\r\n").Select(line => line.Trim()).Where(line => line.Length > 0).ToList(),
            ProjectUrl: (assembly.GetCustomAttributes<AssemblyMetadataAttribute>().Single(attr => attr.Key == "ProjectUrl").Value
                ?? throw new InvalidOperationException())
                    .Split("\r\n").Select(line => line.Trim()).Single(line => line.Length > 0)
        );

        var colorized = new List<string>
        {
            $@"{Green("-----------------------------------------------------------------------")}",
            $@"{Green("-----------------------------------------------------------------------")}",
            $@"               ____  __  ______             __                         ",
            $@"              / __ )/ / / / __ )____ ______/ /____  ______             ",
            $@"             / __  / /_/ / __  / __ `/ ___/ //_/ / / / __ \            ",
            $@"            / /_/ / __  / /_/ / /_/ / /__/ ,< / /_/ / /_/ /            ",
            $@"           /_____/_/ /_/_____/\__,_/\___/_/|_|\__,_/ .___/             ",
            $@"                                                  /_/                  ",
            $@"{Green("-----------------------------------------------------------------------")}",
            $@"{Green("-----------------------------------------------------------------------")}",
            $@"                                                                       ",
            $@"  {Bright.White($"{assemblyInfo.Product} v{assemblyInfo.Version} - {assemblyInfo.Title}")}",
            $@"                                                                       ",
            $@"  {assemblyInfo.Copyright}                                             ",
            $@"  {assemblyInfo.ProjectUrl}                                            ",
            $@"                                                                       "
        }.Concat(
            assemblyInfo.Description.Select(line =>
                $"  {line}"
            )
        ).Concat(
            new[] { "                                                                       " }
        ).Concat(
            assemblyInfo.Disclaimer.Select(line =>
                $"  {Dim(line)}"
            )
        ).ToList();
        foreach (var line in colorized)
        {
            Console.WriteLine(line);
        }
    }

    private static void DoHelp()
    {
        CmdLineArgs.DoBanner();
    }

    private static async Task DoDownload(CmdLineArgs cmdLineArgs)
    {

        CmdLineArgs.DoBanner();
        Console.WriteLine();

        if (string.IsNullOrEmpty(cmdLineArgs.Username) ||
            string.IsNullOrEmpty(cmdLineArgs.Password) ||
            string.IsNullOrEmpty(cmdLineArgs.OutputDirectory))
        {
            Console.WriteLine($"  {Bright.White("Follow the prompts below to start your backup...")}");
            Console.WriteLine();
            Console.WriteLine(
                $@"{Green("-----------------------------------------------------------------------")}"
            );
        }

        if (string.IsNullOrEmpty(cmdLineArgs.Username))
        {
            Console.WriteLine();
            Console.Write(
                Bright.Yellow(
                    "Enter your Bright Horizons FamilyApp username: "
                )
            );
            cmdLineArgs.Username = Console.ReadLine();
        }

        if (string.IsNullOrEmpty(cmdLineArgs.Password))
        {
            Console.WriteLine();
            Console.Write(
                Bright.Yellow(
                    "Enter your Bright Horizons FamilyApp password: "
                )
            );
            // read a masked password string
            // see https://stackoverflow.com/a/3404522/3156906
            var secret = string.Empty;
            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                if ((keyInfo.Key == ConsoleKey.Backspace) && (secret.Length > 0))
                {
                    Console.Write("\b \b");
                    secret = secret[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    secret += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
            }
            cmdLineArgs.Password = secret;
        }

        if (string.IsNullOrEmpty(cmdLineArgs.OutputDirectory))
        {
            var defaultPath = Path.Join(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal
                ),
                $"BHBackup_{DateTime.Now:yyyy_MM_dd}"
            );
            Console.WriteLine();
            Console.WriteLine(
                Bright.Yellow(
                    "Enter the name of the folder to download the offline website to:"
                )
            );
            Console.WriteLine(
                White(
                     $"(press <enter> to use '{defaultPath}')"
                )
            );
            var outputDirectory = Console.ReadLine();
            cmdLineArgs.OutputDirectory = string.IsNullOrEmpty(outputDirectory)
                ? defaultPath
                : outputDirectory;
        }

        // use the anonymous "authenticate" endpoint to log in and get an api access token
        var httpClient = new HttpClient();
        var apiCredentials = await LoginHelper.Authenticate(
            httpClient,
            username: cmdLineArgs.Username ?? throw new InvalidOperationException(),
            password: cmdLineArgs.Password ?? throw new InvalidOperationException(),
            deviceId: Guid.NewGuid().ToString()
        );

        // use the api access token to make any further api calls
        var downloader = new ContentDownloader(
            outputDirectory: cmdLineArgs.OutputDirectory ?? throw new InvalidOperationException(),
            httpClient: httpClient,
            apiCredentials: apiCredentials,
            overwrite: false
        );

        var repositoryFactory = new RepositoryFactory(
            rootFolder: cmdLineArgs.OutputDirectory ?? throw new InvalidOperationException(),
            roundtrip: true
        );

        var download = !cmdLineArgs.SkipDownload;
        var repository = download
            ? downloader.DownloadRepositoryData(repositoryFactory)
            : DataCollection.ReadRepositoryData(repositoryFactory);
        new OfflineUrlVisitor().Visit(repository);
        if (download)
        {
            downloader.DownloadRepositoryContent(repository);
            await downloader.DownloadStaticResources(repository);
        }

        if (!cmdLineArgs.SkipGenerate)
        {
            var htmlWriter = new HtmlWriter(cmdLineArgs.OutputDirectory);
            htmlWriter.GenerateHtmlFiles(repository);
        }

        var indexPath = Path.Join(cmdLineArgs.OutputDirectory, "index.htm");
        _ = Process.Start(
            new ProcessStartInfo(indexPath)
            {
                UseShellExecute = true
            }
        );

        Console.WriteLine();
        Console.WriteLine(
            $@"{Green("-----------------------------------------------------------------------")}"
        );
        Console.WriteLine();

        Console.WriteLine(
            Bright.White(
                "Download complete!"
            )
        );
        Console.WriteLine();
        Console.WriteLine(
            White(
                "You can view the offline website by opening this file in a web browser:"
            )
        );
        Console.WriteLine();
        Console.WriteLine(
            Yellow(indexPath)
        );

        Console.WriteLine();
        Console.WriteLine(
            Bright.White(
                "Press any key to close this program . . ."
            )
        );
        Console.ReadKey();

    }

}
