using BHBackup.Client.GraphQl.LearningJourney.Api;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

internal sealed class LearningJourneyQueryResponse
{

    [JsonPropertyName("data")]
    public LearningJourneyQueryData Data
    {
        get;
        init;
    }

}
