using BHBackup.Client.GraphQl.Identity.Models.Context;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class PublicChild
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
