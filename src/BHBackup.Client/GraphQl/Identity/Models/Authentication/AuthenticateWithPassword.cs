using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Authentication;

[JsonConverter(typeof(AuthenticateWithPasswordConverter))]
public abstract class AuthenticateWithPassword
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(2)]
    public string TypeName
    {
        get;
        init;
    }

    /// <summary>
    /// Known values: "Succeeded", "FailedInvalidPassword"
    /// </summary>
    [JsonPropertyName("status")]
    public string Status
    {
        get;
        init;
    }

}
