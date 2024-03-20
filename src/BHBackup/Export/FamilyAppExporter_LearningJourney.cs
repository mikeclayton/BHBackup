//using BHBackup.Client.GraphQl.ChildNotes.Models;
//using BHBackup.Client.GraphQl.LearningJourney;
//using BHBackup.Client.GraphQl.LearningJourney.Models;
//using BHBackup.Helpers;

//namespace BHBackup.Export;

//internal sealed partial class FamilyAppExporter
//{

//    private IEnumerable<LearningJourneyQueryResponse> DownloadLearningJourneyData()
//    {
//        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
//        // read the learning journey from the api
//        Console.WriteLine("downloading child journey data...");
//        var responses = graphQlClient.PaginateLearningJourneys(
//                childId: "7d991a43-f23f-40e9-8b9b-4c67d7288317",
//                variants: [
//                    "REGULAR_OBSERVATION",
//                    "PARENT_OBSERVATION",
//                    "ASSESSMENT",
//                    "TWO_YEAR_PROGRESS"
//                ],
//                onBeforeRequest: () =>
//                    Console.WriteLine($"    downloading child journey data...")
//            ).ToBlockingEnumerable()
//            .ToList();
//        // save the child notes to disk in individual files
//        foreach (var response in responses)
//        {
//            this.WriteRepositoryJsonFile(
//                OfflinePathHelper.GetLearningJourneyDataFileRelativePath(learningJourney.Data..Id),
//                learningJourney
//            );
//            yield return learningJourney;
//        }
//    }

//}
