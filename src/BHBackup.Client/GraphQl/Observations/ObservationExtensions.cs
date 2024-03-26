using BHBackup.Client.GraphQl.Observations.Api;
using BHBackup.Client.GraphQl.Observations.Queries;
using BHBackup.Common.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl.Observations;

public static class ObservationExtensions
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
                $"{typeof(ObservationQueryResources).Namespace}.{nameof(ObservationsByIds)}.graphql"
            )
        };
        return await graphQlClient.ExecuteGraphQlRequest<ObservationsByIdsResponse>(
            requestUrl: $"/graphql?{nameof(ObservationsByIds)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        ).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<ObservationsByIdsResponse> PaginateObservationsByIds(
        this GraphQlClient graphQlClient,
        IEnumerable<string> observationIds,
        int pageSize,
        Action? onBeforeRequest = null
    )
    {
        ArgumentNullException.ThrowIfNull(graphQlClient);
        var pages = observationIds.Chunk(pageSize).ToList();
        foreach (var page in pages)
        {
            onBeforeRequest?.Invoke();
            var response = await graphQlClient.ObservationsByIds(page).ConfigureAwait(false);
            yield return response;
        }
    }

}
