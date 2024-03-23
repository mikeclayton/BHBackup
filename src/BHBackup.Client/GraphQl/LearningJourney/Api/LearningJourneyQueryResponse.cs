using BHBackup.Client.GraphQl.LearningJourney.Api;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

public sealed class LearningJourneyQueryResponse
{

    [JsonPropertyName("data")]
    public LearningJourneyQueryData Data
    {
        get;
        init;
    }

}
