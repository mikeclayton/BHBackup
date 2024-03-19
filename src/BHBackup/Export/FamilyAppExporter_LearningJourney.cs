using BHBackup.Client.GraphQl.LearningJourney;
using BHBackup.Client.GraphQl.LearningJourney.Models;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private LearningJourneyQueryResponse DownloadLearningJourneyData()
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the learning journey from the api
        Console.WriteLine("downloading learning journey data...");
        var learningJourney = graphQlClient.LearningJourneyQuery(
            childId: "7d991a43-f23f-40e9-8b9b-4c67d7288317",
            variants: [
                "REGULAR_OBSERVATION",
                "PARENT_OBSERVATION",
                "ASSESSMENT",
                "TWO_YEAR_PROGRESS"
            ],
            first: 10,
            next: null
        ).Result;
        return learningJourney;
    }

}
