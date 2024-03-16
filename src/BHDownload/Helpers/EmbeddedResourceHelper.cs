using System.Reflection;
using System.Text;

namespace BHDownload.Helpers;

internal static class EmbeddedResourceHelper
{

    public static string ReadEmbeddedResourceText(Assembly assembly, string resourceName)
    {
        using var resourceStream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new ArgumentException($"Embedded resource '{resourceName}' was not found.", nameof(resourceName));
        using var memoryStream = new MemoryStream();
        resourceStream.CopyTo(memoryStream);
        var resourceText = Encoding.ASCII.GetString(
            memoryStream.ToArray()
        );
        return resourceText;
    }

}
