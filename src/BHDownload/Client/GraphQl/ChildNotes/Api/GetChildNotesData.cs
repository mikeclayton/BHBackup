using BHDownload.Client.GraphQl.ChildNotes.Models;
using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.ChildNotes.Api;

internal sealed class GetChildNotesData
{

    [JsonPropertyName("childNotes")]
    public ChildNotesResult ChildNotes
    {
        get;
        set;
    }

}
