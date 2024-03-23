using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.ChildNotes.Models;

public sealed class ChildNotesChild
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

    [JsonPropertyName("institutionId")]
    public string InstitutionId
    {
        get;
        init;
    }

}
