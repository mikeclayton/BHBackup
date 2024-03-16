using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.ChildNotes.Api;

internal sealed class GetChildNotesResponse
{

    [JsonPropertyName("data")]
    public GetChildNotesData Data
    {
        get;
        set;
    }

}
