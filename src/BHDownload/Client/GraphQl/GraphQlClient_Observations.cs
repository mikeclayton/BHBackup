using BHDownload.Client.GraphQl.Observations.Api;
using BHDownload.Client.GraphQl.Observations.Queries;
using BHDownload.Helpers;
using System.Reflection;

namespace BHDownload.Client.GraphQl;

internal sealed partial class GraphQlClient
{

    public async Task<ObservationsByIdsResponse> ObservationsByIds(IEnumerable<string> observationIds)
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

        return await this.ExecuteGraphQlRequest<ObservationsByIdsResponse>(
            requestUrl: $"/graphql?{nameof(GetChildNotes)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );

    }

}
