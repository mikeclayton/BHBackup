using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

public sealed class FeedImage
{

    [JsonPropertyName("imageId")]
    public string ImageId
    {
        get;
        init;
    }

    [JsonPropertyName("url")]
    public string Url
    {
        get;
        init;
    }

    [JsonPropertyName("url_big")]
    public string UrlBig
    {
        get;
        init;
    }

    [JsonPropertyName("dim")]
    public int[] Size
    {
        get;
        init;
    }

    [JsonPropertyName("dim_big")]
    public int[] SizeBig
    {
        get;
        init;
    }

    [JsonPropertyName("thumbnail")]
    public FeedImageInfo Thumbnail
    {
        get;
        init;
    }

    [JsonPropertyName("big")]
    public FeedImageInfo Big
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

    [JsonPropertyName("prefix")]
    public string Prefix
    {
        get;
        init;
    }

    [JsonPropertyName("key")]
    public string Key
    {
        get;
        init;
    }

    [JsonPropertyName("height")]
    public int Height
    {
        get;
        init;
    }

    [JsonPropertyName("width")]
    public int Width
    {
        get;
        init;
    }

    [JsonPropertyName("expiration")]
    public string Expiration
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

    [JsonPropertyName("tags")]
    public object Tags
    {
        get;
        init;
    }

    [JsonIgnore]
    public string FullSizeUrl
        => $"{this.Prefix}/{this.Width}x{this.Height}/{this.Key}";

    [JsonIgnore]
    public string OfflineUrl
    {
        get;
        set;
    }

}
