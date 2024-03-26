using BHBackup.Client.GraphQl.LearningJourney;
using BHBackup.Client.GraphQl.LearningJourney.Models;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async IAsyncEnumerable<LearningJourneyQueryResponse> DownloadChildJourneyData(
        LearningJourneyRepository repository, IEnumerable<string> childIds, IEnumerable<string> variants)
    {
        var graphQlClient = this.GetGraphQlClient();
        // read the learning journey from the api
        this.Logger.LogInformation("downloading learning journey data...");
        var counter = 1;
        foreach (var childId in childIds)
        {
            var responses = graphQlClient.PaginateLearningJourneys(
                childId: childId, variants
            ).ConfigureAwait(false);
            // save the journeys to disk in individual files
            await foreach (var response in responses)
            {
                repository.WriteItem(response, counter);
                yield return response;
                counter++;
            }
        }
    }

}
