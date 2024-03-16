using BHDownload.Client.ApiV1.Feeds.Models;
using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.ChildNotes.Models;

internal sealed class ChildNote
{

    [JsonPropertyName("__typename")]
    public string Typename
    {
        get;
        init;
    }

    [JsonPropertyName("noteType")]
    public string NoteType
    {
        get;
        init;
    }

    [JsonPropertyName("child")]
    public ChildNotesChild Child
    {
        get;
        init;
    }

    [JsonPropertyName("modifiedAt")]
    public string ModifiedAt
    {
        get;
        init;
    }

    [JsonPropertyName("modifiedBy")]
    public ChildNotesPerson ModifiedBy
    {
        get;
        init;
    }

    [JsonPropertyName("createdBy")]
    public ChildNotesPerson CreatedBy
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

    [JsonPropertyName("parentVisible")]
    public bool ParentVisible
    {
        get;
        init;
    }

    [JsonPropertyName("sensitive")]
    public bool Sensitive
    {
        get;
        init;
    }

    [JsonPropertyName("text")]
    public string Text
    {
        get;
        init;
    }

    [JsonPropertyName("safeguardingConcern")]
    public bool SafeguardingConcern
    {
        get;
        init;
    }

    [JsonPropertyName("createdAt")]
    public string CreatedAt
    {
        get;
        init;
    }

    [JsonIgnore]
    public DateTime CreatedAtParsed =>
        DateTime.Parse(this.CreatedAt);

    [JsonPropertyName("publishedAt")]
    public string PublishedAt
    {
        get;
        init;
    }

    [JsonPropertyName("files")]
    public List<FeedFile> Files
    {
        get;
        init;
    }

    [JsonPropertyName("images")]
    public List<ChildNotesImage> Images
    {
        get;
        init;
    }

    [JsonPropertyName("behaviors")]
    public object Behaviors
    {
        get;
        init;
    }

    [JsonPropertyName("parentAcknowledgements")]
    public object ParentAcknowledgements
    {
        get;
        init;
    }

}
