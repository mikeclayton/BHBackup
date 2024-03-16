using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

internal sealed class FeedItem
{

    [JsonPropertyName("systemPostTypeClass")]
    public string SystemPostTypeClass
    {
        get;
        init;
    }

    [JsonPropertyName("systemPostTypeIcon")]
    public string SystemPostTypeIcon
    {
        get;
        init;
    }

    [JsonPropertyName("sender")]
    public FeedSender Sender
    {
        get;
        init;
    }

    [JsonPropertyName("receivers")]
    public List<string> Receivers
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

    [JsonPropertyName("images")]
    public List<FeedImage> Images
    {
        get;
        init;
    }

    [JsonPropertyName("thumbs")]
    public List<string> Thumbs
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

    [JsonPropertyName("videos")]
    public List<object> Videos
    {
        get;
        init;
    }

    [JsonPropertyName("disableFeedback")]
    public bool DisableFeedback
    {
        get;
        init;
    }

    [JsonPropertyName("embed")]
    public FeedEmbed? Embed
    {
        get;
        init;
    }

    [JsonPropertyName("originatorId")]
    public string OriginatorId
    {
        get;
        init;
    }

    [JsonPropertyName("siteSetId")]
    public string SiteSetId
    {
        get;
        init;
    }

    [JsonPropertyName("famlyMeetingId")]
    public object FamlyMeetingId
    {
        get;
        init;
    }

    [JsonPropertyName("canReport")]
    public bool CanReport
    {
        get;
        init;
    }

    [JsonPropertyName("feedItemId")]
    public string FeedItemId
    {
        get;
        init;
    }

    [JsonPropertyName("createdDate")]
    public string CreatedDate
    {
        get;
        init;
    }

    [JsonIgnore]
    public DateTime CreatedDateParsed =>
        DateTime.Parse(this.CreatedDate);

    [JsonPropertyName("seen")]
    public bool Seen
    {
        get;
        init;
    }

    [JsonPropertyName("userContext")]
    public string UserContext
    {
        get;
        init;
    }

    [JsonPropertyName("canDelete")]
    public bool CanDelete
    {
        get;
        init;
    }

    [JsonPropertyName("canBookmark")]
    public bool CanBookmark
    {
        get;
        init;
    }

    [JsonPropertyName("generated")]
    public bool Generated
    {
        get;
        init;
    }

    [JsonPropertyName("canEdit")]
    public bool CanEdit
    {
        get;
        init;
    }

    [JsonPropertyName("comments")]
    public List<object> Comments
    {
        get;
        init;
    }

    [JsonPropertyName("likes")]
    public object Likes
    {
        get;
        init;
    }

    [JsonPropertyName("liked")]
    public bool Liked
    {
        get;
        init;
    }

    [JsonPropertyName("seenLogins")]
    public object SeenLogins
    {
        get;
        init;
    }

    [JsonPropertyName("stats")]
    public object Stats
    {
        get;
        init;
    }

    [JsonPropertyName("bookmarked")]
    public bool Bookmarked
    {
        get;
        init;
    }

    [JsonPropertyName("canBlock")]
    public bool CanBlock
    {
        get;
        init;
    }

    public DateTime ParseCreatedDate()
    {
        return DateTime.Parse(this.CreatedDate);
    }

}
