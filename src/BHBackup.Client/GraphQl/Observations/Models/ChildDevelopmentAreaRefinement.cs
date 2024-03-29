﻿using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class ChildDevelopmentAreaRefinement
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("area")]
    public Area Area
    {
        get;
        init;
    }

    /// <remarks>
    /// Known values: "SECURE"
    /// </remarks>
    [JsonPropertyName("refinement")]
    public string Refinement
    {
        get;
        init;
    }

    [JsonPropertyName("note")]
    public string Note
    {
        get;
        init;
    }

    [JsonPropertyName("areaRefinementSettings")]
    public ChildDevelopmentAreaRefinementSetting AreaRefinementSettings
    {
        get;
        init;
    }

}
