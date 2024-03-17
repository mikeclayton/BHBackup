using System.Text.Json;

namespace BHBackup.Helpers;

internal static class JsonHelper
{

    private static JsonSerializerOptions PrettifierOptions =>
        new JsonSerializerOptions
        {
            WriteIndented = true
        };

    public static string Prettify(string unformattedJson)
    {
        using var document = JsonDocument.Parse(unformattedJson);
        // format the json response
        return JsonSerializer.Serialize(
            document.RootElement, JsonHelper.PrettifierOptions
        );
    }

    public static T ConvertFromJson<T>(string json, bool roundtrip)
    {
        // parse the json
        var result = JsonSerializer.Deserialize<T>(json)
            ?? throw new InvalidOperationException();
        // make sure the object roundtrips back to the original json
        if (roundtrip)
        {
            var prettyJson = JsonHelper.Prettify(json);
            var roundtripJson = JsonHelper.ConvertToJson(result);
            if (roundtripJson != prettyJson)
            {
                throw new InvalidOperationException();
            }
        }
        return result;
    }

    public static string? ConvertToJson<T>(T? value)
    {
        if (value is null)
        {
            return null;
        }
        // format the json response
        return JsonSerializer.Serialize(
            value, JsonHelper.PrettifierOptions
        );
    }

}
