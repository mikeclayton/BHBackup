using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.ChildNotes.Models;

internal sealed class ChildNotesResult
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
