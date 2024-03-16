using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

internal sealed class FeedEmbedDaycareEvent : FeedEmbed
{

    /// <remarks>
    /// Known values:
    ///   * Daycare.Event
    ///   * Observation
    /// </remarks>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(99)]
    public string Type
    {
        get;
        init;
    }

    [JsonPropertyName("eventId")]
    public string EventId
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

    [JsonPropertyName("creatorLoginId")]
    public string CreatorLoginId
    {
        get;
        init;
    }

    [JsonPropertyName("title")]
    public string Title
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

    [JsonPropertyName("fromTime")]
    public string FromTime
    {
        get;
        init;
    }

    [JsonIgnore]
    public DateTime FromTimeParsed
        => DateTime.Parse(this.FromTime);

    [JsonPropertyName("toTime")]
    public string ToTime
    {
        get;
        init;
    }

    [JsonIgnore]
    public DateTime ToTimeParsed
        => DateTime.Parse(this.ToTime);

    [JsonPropertyName("rsvp")]
    public int Rsvp
    {
        get;
        init;
    }

    [JsonPropertyName("deadline")]
    public int Deadline
    {
        get;
        init;
    }

    [JsonPropertyName("imageId")]
    public string ImageId
    {
        get;
        init;
    }

    [JsonPropertyName("image")]
    public object Image
    {
        get;
        init;
    }

    [JsonPropertyName("recipients")]
    public List<object> Recipients
    {
        get;
        init;
    }

    [JsonPropertyName("noChildren")]
    public int NoChildren
    {
        get;
        init;
    }

    [JsonPropertyName("noAdults")]
    public int NoAdults
    {
        get;
        init;
    }

    [JsonPropertyName("noMissingReplies")]
    public int NoMissingReplies
    {
        get;
        init;
    }

    [JsonPropertyName("deletedAt")]
    public object DeletedAt
    {
        get;
        init;
    }

    [JsonPropertyName("createdAt")]
    public FeedDateTime CreatedAt
    {
        get;
        init;
    }

    [JsonPropertyName("timezone")]
    public string Timezone
    {
        get;
        init;
    }

    [JsonPropertyName("postToFeed")]
    public bool PostedToFeed
    {
        get;
        init;
    }

}
