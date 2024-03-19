using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.ChildSummary.Models;

internal sealed class SummaryInstitution
{

    [JsonPropertyName("institutionId")]
    public string InstitutionId
    {
        get;
        init;
    }

    [JsonPropertyName("organizationId")]
    public string OrganizationId
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

    [JsonPropertyName("usePincodeCheckin")]
    public bool UsePinCodeCheckin
    {
        get;
        init;
    }

    [JsonPropertyName("country")]
    public string Country
    {
        get;
        init;
    }

    [JsonPropertyName("locale")]
    public string Locale
    {
        get;
        init;
    }

    [JsonPropertyName("features")]
    public List<string> Features
    {
        get;
        init;
    }

    [JsonPropertyName("behaviors")]
    public List<SummaryBehavior> Behaviors
    {
        get;
        init;
    }

    [JsonPropertyName("timezone")]
    public string Timezone
    {
        get;
        init;
    }

    [JsonPropertyName("image")]
    public string Image
    {
        get;
        init;
    }

    [JsonPropertyName("famlyId")]
    public string FamilyId
    {
        get;
        init;
    }

}
