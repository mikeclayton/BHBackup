using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV2.Models;

internal sealed class SummaryChildName
{

    [JsonPropertyName("salutation")]
    public string Salutation
    {
        get;
        init;
    }

    [JsonPropertyName("firstName")]
    public string FirstName
    {
        get;
        init;
    }

    [JsonPropertyName("middleName")]
    public string MiddleName
    {
        get;
        init;
    }

    [JsonPropertyName("lastName")]
    public string LastName
    {
        get;
        init;
    }

    [JsonPropertyName("fullName")]
    public string FullName
    {
        get;
        init;
    }

    [JsonPropertyName("fullNameInOwnersForm")]
    public string FullNameInOwnersForm
    {
        get;
        init;
    }

}
