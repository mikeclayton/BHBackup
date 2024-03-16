using BHDownload.Client.GraphQl.Identity.Models;
using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class PublicChild
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
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


    [JsonPropertyName("name")]
    public string Name
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


    [JsonPropertyName("profileImage")]
    public ProfileImage ProfileImage
    {
        get;
        init;
    }

}
