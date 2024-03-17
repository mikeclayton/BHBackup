using BHBackup.Client.GraphQl.ChildNotes.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.ChildNotes.Api;

internal sealed class GetChildNotesData
{

    [JsonPropertyName("childNotes")]
    public ChildNotesResult ChildNotes
    {
        get;
        set;
    }

}
