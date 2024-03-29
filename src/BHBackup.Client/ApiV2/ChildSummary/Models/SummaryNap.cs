﻿using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.ChildSummary.Models;

public sealed class SummaryNap
{

    [JsonPropertyName("startTime")]
    public string StartTime
    {
        get;
        init;
    }

    [JsonPropertyName("duration")]
    public decimal Duration
    {
        get;
        init;
    }

}
