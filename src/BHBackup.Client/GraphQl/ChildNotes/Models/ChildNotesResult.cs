using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.ChildNotes.Models;

public sealed class ChildNotesResult
{

    [JsonPropertyName("__typename")]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("next")]
    public string? Next
    {
        get;
        set;
    }


    [JsonPropertyName("result")]
    public List<ChildNote> Result
    {
        get;
        set;
    }

}
