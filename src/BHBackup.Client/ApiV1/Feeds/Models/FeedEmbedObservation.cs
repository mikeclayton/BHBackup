﻿using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

public sealed class FeedEmbedObservation : FeedEmbed
{

    /// <remarks>
    /// Known values:
    ///   * Daycare.Event
    ///   * Observation
    /// </remarks>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type
    {
        get;
        init;
    }

    [JsonPropertyName("childId")]
    public string ChildId
    {
        get;
        init;
    }

    [JsonPropertyName("childIds")]
    public string[] ChildIds
    {
        get;
        init;
    }

    [JsonPropertyName("observationId")]
    public string ObservationId
    {
        get;
        init;
    }

}
