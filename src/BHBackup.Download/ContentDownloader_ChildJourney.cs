using BHBackup.Client.GraphQl.LearningJourney;
using BHBackup.Client.GraphQl.LearningJourney.Models;
using BHBackup.Storage.Repositories;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public IEnumerable<LearningJourneyQueryResponse> DownloadChildJourneyData(
        LearningJourneyRepository repository, IEnumerable<string> childIds, IEnumerable<string> variants)
    {
        var graphQlClient = this.GetGraphQlClient();
        // read the learning journey from the api
        Console.WriteLine("downloading learning journey data...");
        var counter = 1;
        foreach (var childId in childIds)
        {
            var journeys = graphQlClient.PaginateLearningJourneys(
                childId: childId, variants
            ).ToBlockingEnumerable().ToList();
            // save the journeys to disk in individual files
            foreach (var journey in journeys)
            {
                repository.WriteItem(journey, counter);
                yield return journey;
                counter++;
            }
        }
    }

}
