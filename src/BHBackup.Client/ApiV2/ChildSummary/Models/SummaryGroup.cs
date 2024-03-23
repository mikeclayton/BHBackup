using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.ChildSummary.Models;

public sealed class SummaryGroup
{

    [JsonPropertyName("groupId")]
    public string GroupId
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

    [JsonPropertyName("departmentId")]
    public string DepartmentId
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

    [JsonPropertyName("info")]
    public string Info
    {
        get;
        init;
    }

    /// <summary>
    /// Known values: "GRAY"
    /// </summary>
    [JsonPropertyName("color")]
    public string Golor
    {
        get;
        init;
    }

    [JsonPropertyName("phone")]
    public string Phone
    {
        get;
        init;
    }

    [JsonPropertyName("requirePickupInfo")]
    public bool RequirePickupInfo
    {
        get;
        init;
    }

    [JsonPropertyName("deletedAt")]
    public object DeletedAt
    {
        get;
        init;
    }

    [JsonPropertyName("capacity")]
    public object Capacity
    {
        get;
        init;
    }

    [JsonPropertyName("staffRatio")]
    public object StaffRatio
    {
        get;
        init;
    }

    [JsonPropertyName("ordering")]
    public object Ordering
    {
        get;
        init;
    }

}
