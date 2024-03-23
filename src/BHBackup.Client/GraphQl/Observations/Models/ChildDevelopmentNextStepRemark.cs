using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class ChildDevelopmentNextStepRemark
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("body")]
    public string Body
    {
        get;
        init;
    }

    [JsonPropertyName("richTextBody")]
    public string RichTextBody
    {
        get;
        init;
    }

}
