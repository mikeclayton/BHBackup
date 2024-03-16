using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV2.Models;

internal sealed class SummaryChild
{

    [JsonPropertyName("childId")]
    public string ChildId
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

    [JsonPropertyName("groupId")]
    public string GroupId
    {
        get;
        init;
    }

    [JsonPropertyName("createdTime")]
    public string CreatedTime
    {
        get;
        init;
    }

    [JsonPropertyName("name")]
    public SummaryChildName Name
    {
        get;
        init;
    }

    [JsonPropertyName("birthday")]
    public string Birthday
    {
        get;
        init;
    }

    [JsonPropertyName("homeAddress")]
    public object HomeAddress
    {
        get;
        init;
    }

    [JsonPropertyName("extraInfo")]
    public string ExtraInfo
    {
        get;
        init;
    }

    [JsonPropertyName("language")]
    public string Language
    {
        get;
        init;
    }

    [JsonPropertyName("nationality")]
    public object Nationality
    {
        get;
        init;
    }

    [JsonPropertyName("birthplace")]
    public object Birthplace
    {
        get;
        init;
    }

    [JsonPropertyName("gender")]
    public string Gender
    {
        get;
        init;
    }

    [JsonPropertyName("startDate")]
    public string StartDate
    {
        get;
        init;
    }

    [JsonPropertyName("endDate")]
    public string EndDate
    {
        get;
        init;
    }

    [JsonPropertyName("leavingReason")]
    public object LeavingReason
    {
        get;
        init;
    }

    [JsonPropertyName("isTestChild")]
    public bool IsTestChild
    {
        get;
        init;
    }

    [JsonPropertyName("famlyId")]
    public string FamlyId
    {
        get;
        init;
    }

    [JsonPropertyName("genderId")]
    public int GenderId
    {
        get;
        init;
    }

    [JsonPropertyName("localStartDate")]
    public string LocalStartDate
    {
        get;
        init;
    }

    [JsonPropertyName("localEndDate")]
    public string LocalEndDate
    {
        get;
        init;
    }

    [JsonPropertyName("image")]
    public SummaryImage Image
    {
        get;
        init;
    }

    [JsonPropertyName("onApp")]
    public bool OnApp
    {
        get;
        init;
    }

    [JsonPropertyName("tagIds")]
    public List<object> TagIds
    {
        get;
        init;
    }

    [JsonPropertyName("groupTitle")]
    public string GroupTitle
    {
        get;
        init;
    }

    [JsonPropertyName("isSleeping")]
    public bool IsSleeping
    {
        get;
        init;
    }

    [JsonPropertyName("naps")]
    public List<object> Naps
    {
        get;
        init;
    }

    [JsonPropertyName("hasVacation")]
    public bool HasVacation
    {
        get;
        init;
    }

    [JsonPropertyName("isSick")]
    public bool IsSick
    {
        get;
        init;
    }

    [JsonPropertyName("isAbsent")]
    public bool IsAbsent
    {
        get;
        init;
    }

    [JsonPropertyName("leaves")]
    public List<object> Leaves
    {
        get;
        init;
    }

    [JsonPropertyName("onBus")]
    public bool OnBus
    {
        get;
        init;
    }

    [JsonPropertyName("onTrip")]
    public bool OnTrip
    {
        get;
        init;
    }

    [JsonPropertyName("statuses")]
    public List<object> Statuses
    {
        get;
        init;
    }

    [JsonPropertyName("checkins")]
    public List<object> Checkins
    {
        get;
        init;
    }

    [JsonPropertyName("checkedIn")]
    public bool CheckedIn
    {
        get;
        init;
    }

    [JsonPropertyName("checkinTime")]
    public string CheckinTime
    {
        get;
        init;
    }


    [JsonPropertyName("pickupTime")]
    public string PickupTine
    {
        get;
        init;
    }

    [JsonPropertyName("pickupRelationId")]
    public string PickupRelationId
    {
        get;
        init;
    }

    [JsonPropertyName("pickupName")]
    public string PickupName
    {
        get;
        init;
    }

    [JsonPropertyName("keyWorkers")]
    public List<object> KeyWorkers
    {
        get;
        init;
    }

    [JsonPropertyName("actions")]
    public List<object> Actions
    {
        get;
        init;
    }

    [JsonPropertyName("security2")]
    public bool Security2
    {
        get;
        init;
    }

    [JsonPropertyName("mealRegistrations")]
    public List<object> MealRegistrations
    {
        get;
        init;
    }


    [JsonPropertyName("mealRegistrationsToday")]
    public int MealRegistrationsToday
    {
        get;
        init;
    }

    [JsonPropertyName("movingTo")]
    public object MovingTo
    {
        get;
        init;
    }

    [JsonPropertyName("localMovingToDate")]
    public string LocalMovingToDate
    {
        get;
        init;
    }

    [JsonPropertyName("roles2")]
    public List<object> Roles2
    {
        get;
        init;
    }


    [JsonPropertyName("deletion")]
    public object Deletion
    {
        get;
        init;
    }

    [JsonPropertyName("conversation")]
    public object Conversation
    {
        get;
        init;
    }

    [JsonPropertyName("attending")]
    public object Attending
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

    [JsonPropertyName("statusRegistrations")]
    public List<object> StatusRegistrations
    {
        get;
        init;
    }

}
