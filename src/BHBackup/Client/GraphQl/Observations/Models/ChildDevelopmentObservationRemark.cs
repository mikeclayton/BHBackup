using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ChildDevelopmentObservationRemark
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

    [JsonPropertyName("date")]
    public string Date
    {
        get;
        init;
    }

    [JsonIgnore]
    public DateTime DateParsed
        => DateTime.Parse(this.Date);

    [JsonPropertyName("statements")]
    public object Statements
    {
        get;
        init;
    }

    [JsonPropertyName("areas")]
    public List<ChildDevelopmentAreaRefinement> Areas
    {
        get;
        init;
    }

    [JsonPropertyName("customFieldValues")]
    public object CustomFieldValues
    {
        get;
        init;
    }

}
