using BHBackup.Client.GraphQl.LearningJourney.Models;
using BHBackup.Client.GraphQl.LearningJourney.Queries;
using BHBackup.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl.LearningJourney;

internal static class LearningJourneyExtensions
{

    /// <param name="graphQlClient"></param>
    /// <param name="childId"></param>
    /// <param name="variants">
    /// Known values "REGULAR_OBSERVATION", "PARENT_OBSERVATION", "ASSESSMENT", "TWO_YEAR_PROGRESS"
    /// </param>
    /// <param name="first"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public static async Task<LearningJourneyQueryResponse> LearningJourneyQuery(
        this GraphQlClient graphQlClient,
        string childId,
        IEnumerable<string> variants,
        int first,
        string next
    )
    {
        var requestBody = new
        {
            operationName = nameof(LearningJourneyQuery),
            variables = new
            {
                childId = childId,
                variants = variants,
                first = first,
                next = next
            },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                    $"{typeof(EmbeddedResources).Namespace}.{nameof(LearningJourneyQuery)}.graphql"
            )
        };
        return await graphQlClient.ExecuteGraphQlRequest<LearningJourneyQueryResponse>(
            requestUrl: $"/graphql?{nameof(LearningJourneyQuery)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );
    }

}
