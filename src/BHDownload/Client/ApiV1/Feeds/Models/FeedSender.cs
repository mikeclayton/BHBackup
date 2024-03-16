﻿using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

internal sealed class FeedSender
{

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("loginId")]
    public string LoginId
    {
        get;
        init;
    }

    [JsonPropertyName("profileImage")]
    public string ProfileImage
    {
        get;
        init;
    }

    [JsonIgnore]
    public string OfflineUrl
    {
        get;
        set;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get;
        init;
    }

    [JsonPropertyName("subtitle")]
    public string Subtitle
    {
        get;
        init;
    }

}
