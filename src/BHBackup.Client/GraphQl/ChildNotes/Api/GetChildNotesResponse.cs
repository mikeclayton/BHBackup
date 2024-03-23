using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.ChildNotes.Api;

public sealed class GetChildNotesResponse
{

    [JsonPropertyName("data")]
    public GetChildNotesData Data
    {
        get;
        set;
    }

}
