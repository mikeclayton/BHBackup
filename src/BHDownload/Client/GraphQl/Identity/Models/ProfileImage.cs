﻿using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Models;

internal sealed class ProfileImage
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
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

    [JsonIgnore]
    public string OfflineUrl
    {
        get;
        set;
    }

}
