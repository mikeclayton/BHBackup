using BHDownload.Helpers;
using System.Reflection;
using BHDownload.Static.Assets;

namespace BHDownload.Export;

internal sealed partial class FamilyAppExporter
{

    private async Task DownloadHttpResource(string resourceUri, string relativePath, bool overwrite)
    {
        ArgumentNullException.ThrowIfNull(resourceUri);
        ArgumentNullException.ThrowIfNull(relativePath);
        // overwrite existing file?
        var absolutePath = this.GetAbsoluteFilename(relativePath);
        if (!overwrite && File.Exists(absolutePath))
        {
            Console.WriteLine($"    skipping '{relativePath}'...");
            return;
        }
        // create the destination directory if it doesn't already exist
        Directory.CreateDirectory(
            Path.GetDirectoryName(absolutePath) ?? throw new InvalidOperationException()
        );
        // build the request message
        var request = new HttpRequestMessage(HttpMethod.Get, resourceUri);
        request.Headers.ConnectionClose = false;
        // download the resource
        Console.WriteLine($"    downloading '{relativePath}'...");
        var response = await this.HttpClient.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream);
    }

    private async Task DownloadStaticResources(IEnumerable<string> resourceUris, bool overwrite)
    {
        var domainPathMap = new Dictionary<string, string>
        {
            ["https://familyapp.brighthorizons.co.uk/"] = "familyapp",
            ["https://static.famly.co/"] = "famly.co"
        };
        foreach (var resourceUri in resourceUris)
        {
            var domainPathKey = domainPathMap.Keys
                .FirstOrDefault(
                    key => resourceUri.StartsWith(key)
                ) ?? throw new InvalidOperationException();
            // get the relative path to save the resource as
            // (resource uri might have a querystring, so we need to remove that)
            var targetRoot = domainPathMap[domainPathKey];
            var targetPath = (new Uri(resourceUri).AbsolutePath)[
                new Uri(domainPathKey).AbsolutePath.Length..
            ].Replace("/", "\\");
            var targetFullname = Path.Join(
                targetRoot, targetPath
            );
            await this.DownloadHttpResource(
                resourceUri, targetFullname, overwrite
            );
        }
    }

    private async Task DownloadStaticHttpFonts(bool overwrite)
    {
        var resourceUris = new[]
        {
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-BoldItalic.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-BoldItalic.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Italic.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Italic.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Medium.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Medium.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-MediumItalic.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-MediumItalic.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Regular.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-Regular.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-SemiBold.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-SemiBold.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-SemiBoldItalic.woff?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Inter/Inter-SemiBoldItalic.woff2?v=3.19",
            "https://familyapp.brighthorizons.co.uk/fonts/Material-Symbols/Material-Symbols-Rounded-Wght-300.woff2?v=3",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Bold.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Bold.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Bold.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Bold.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-BoldItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-BoldItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-BoldItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-BoldItalic.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Heavy.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Heavy.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Heavy.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Heavy.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-HeavyItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-HeavyItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-HeavyItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-HeavyItalic.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-LightItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-LightItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-LightItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-LightItalic.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Medium.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Medium.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Medium.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Medium.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-MediumItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-MediumItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-MediumItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-MediumItalic.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Regular.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Regular.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Regular.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-Regular.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-RegularItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-RegularItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-RegularItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-RegularItalic.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBold.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBold.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBold.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBold.woff2",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBoldItalic.eot",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBoldItalic.ttf",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBoldItalic.woff",
            "https://familyapp.brighthorizons.co.uk/fonts/Matter/Matter-SemiBoldItalic.woff2"
        };
        Console.WriteLine("downloading static fonts...");
        await this.DownloadStaticResources(resourceUris, overwrite);
    }


    private async Task DownloadStaticHttpImages(bool overwrite)
    {
        var resourceUris = new[]
        {
            "https://familyapp.brighthorizons.co.uk/img/icons/calendar.png",
            "https://familyapp.brighthorizons.co.uk/img/icons/tap.png",
            "https://static.famly.co/core/feed-icons/checkin.png",
            "https://static.famly.co/core/feed-icons/checkout.png"
        };
        Console.WriteLine("downloading static images...");
        await this.DownloadStaticResources(resourceUris, overwrite);
    }

    private void UnpackEmbeddedStylesheets(bool overwrite)
    {

        var assembly = Assembly.GetExecutingAssembly();
        var stylesheets = Assembly.GetExecutingAssembly()
            .GetManifestResourceNames()
            .Where(
                name => name.EndsWith(".css")
            ).ToList();

        Console.WriteLine("unpacking embedded stylesheets...");
        var prefix = typeof(EmbeddedResources).Namespace + ".";
        foreach (var stylesheet in stylesheets)
        {
            // work out the target filename
            if (!stylesheet.StartsWith(prefix))
            {
                throw new InvalidOperationException();
            }
            var resourceText = EmbeddedResourceHelper.ReadEmbeddedResourceText(assembly, stylesheet);

            var targetRelativeFilename = OfflinePathHelper.GetAssetResourceFileRelativePath(stylesheet[(prefix.Length)..]);
            var targetAbsoluteFilename = this.GetAbsoluteFilename(targetRelativeFilename);

            if (!overwrite && File.Exists(targetAbsoluteFilename))
            {
                Console.WriteLine($"    skipping '{targetRelativeFilename}'...");
                continue;
            }

            Console.WriteLine($"    unpacking '{targetRelativeFilename}'...");
            Directory.CreateDirectory(
                Path.GetDirectoryName(targetAbsoluteFilename) ?? throw new InvalidOperationException()
            );
            File.WriteAllText(targetAbsoluteFilename, resourceText);

        }

    }

}