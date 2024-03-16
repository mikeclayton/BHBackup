using BHDownload.Client.GraphQl.Identity.Models;
using System.Text.Json.Serialization;
using BHDownload.Client.ApiV1.Feeds.Models;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class Observation
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("children")]
    public List<PublicChild> Children
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

    /// <remarks>
    /// Known values: "EYFS_2021_REVISION"
    /// </remakrs>
    [JsonPropertyName("version")]
    public string Version
    {
        get;
        init;
    }

    [JsonPropertyName("feedItem")]
    public ObservationFeedItem FeedItem
    {
        get;
        init;
    }

    [JsonPropertyName("createdBy")]
    public ObservationPerson CreatedBy
    {
        get;
        init;
    }

    [JsonPropertyName("status")]
    public ObservationStatus Status
    {
        get;
        init;
    }

    /// <remarks>
    /// Known values: "REGULAR_OBSERVATION", "ASSESSMENT"
    /// </remarks>
    [JsonPropertyName("variant")]
    public string Variant
    {
        get;
        init;
    }

    [JsonPropertyName("settings")]
    public ObservationSettings Settings
    {
        get;
        init;
    }

    [JsonPropertyName("behaviors")]
    public object Behaviurs
    {
        get;
        init;
    }

    [JsonPropertyName("remark")]
    public ChildDevelopmentObservationRemark Remark
    {
        get;
        init;
    }

    [JsonPropertyName("nextStep")]
    public ChildDevelopmentNextStepRemark NextStep
    {
        get;
        init;
    }

    [JsonPropertyName("files")]
    public List<object> Files
    {
        get;
        init;
    }

    [JsonPropertyName("images")]
    public List<Image> Images
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

    [JsonPropertyName("likes")]
    public object Likes
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

    [JsonPropertyName("comments")]
    [JsonPropertyOrder(100)]
    public ObservationComments Comments
    {
        get;
        init;
    }

}
