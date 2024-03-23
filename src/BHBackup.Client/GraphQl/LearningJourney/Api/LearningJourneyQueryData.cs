using BHBackup.Client.GraphQl.LearningJourney.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Api;

public sealed class LearningJourneyQueryData
{

    [JsonPropertyName("childDevelopment")]
    public ChildDevelopment ChildDevelopment
    {
        get;
        init;
    }

}
