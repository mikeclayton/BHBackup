using BHBackup.Client.GraphQl.LearningJourney.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Api;

internal sealed class LearningJourneyQueryData
{

    [JsonPropertyName("childDevelopment")]
    public ChildDevelopment ChildDevelopment
    {
        get;
        init;
    }

}
