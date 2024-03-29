﻿using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class AssessmentSettings
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("assessmentSettingsId")]
    public string AssessmentSettingsId
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

}
