using BHBackup.Client.GraphQl.Observations.Queries;
using BHBackup.Client.GraphQl.Observations.Api;
using BHBackup.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl.Observations;

internal static class ObservationExtensions
{

    public static async Task<ObservationsByIdsResponse> ObservationsByIds(
        this GraphQlClient graphQlClient,
        IEnumerable<string> observationIds
    )
    {
        ArgumentNullException.ThrowIfNull(observationIds);
        var requestUri = $"/graphql?{nameof(ObservationsByIds)}";
        var requestBody = new
        {
            operationName = nameof(ObservationsByIds),
            variables = new
            {
                observationIds = observationIds
            },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                $"{typeof(EmbeddedResources).Namespace}.{nameof(ObservationsByIds)}.graphql"
            )
        };
        return await graphQlClient.ExecuteGraphQlRequest<ObservationsByIdsResponse>(
            requestUrl: $"/graphql?{nameof(ObservationsByIds)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );
    }

}
