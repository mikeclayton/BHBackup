using BHBackup.Client.GraphQl.ChildNotes.Api;
using BHBackup.Client.GraphQl.ChildNotes.Queries;
using BHBackup.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl;

internal sealed partial class GraphQlClient
{

    public async Task<GetChildNotesResponse> GetChildNotes(
        string childId,
        IEnumerable<string> noteTypes,
        int limit,
        bool parentVisible,
        bool safeguardingConcerns,
        bool sensitive,
        string? cursor = null
    )
    {

        var requestBody = new
        {
            operationName = nameof(GetChildNotes),
            variables = new
            {
                childId = childId,
                cursor = cursor,
                limit = limit,
                noteTypes = noteTypes,
                parentVisible = parentVisible,
                safeguardingConcerns = safeguardingConcerns,
                sensitive = sensitive
            },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                    $"{typeof(EmbeddedResources).Namespace}.{nameof(GetChildNotes)}.graphql"
            )
        };

        return await this.ExecuteGraphQlRequest<GetChildNotesResponse>(
            requestUrl: $"/graphql?{nameof(GetChildNotes)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );

    }

}
