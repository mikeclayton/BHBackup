using BHBackup.Client.GraphQl.ChildNotes.Api;
using BHBackup.Client.GraphQl.ChildNotes.Queries;
using BHBackup.Common.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl.ChildNotes;

public static class ChildNotesExtensions
{

    public static async Task<GetChildNotesResponse> GetChildNotes(
        this GraphQlClient graphQlClient,
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
                    $"{typeof(ChildNotesQueryResources).Namespace}.{nameof(GetChildNotes)}.graphql"
            )
        };
        return await graphQlClient.ExecuteGraphQlRequest<GetChildNotesResponse>(
            requestUrl: $"/graphql?{nameof(GetChildNotes)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );
    }

    public static async IAsyncEnumerable<GetChildNotesResponse> PaginateChildNotes(
        this GraphQlClient graphQlClient,
        string childId,
        IEnumerable<string> noteTypes,
        int limit,
        bool parentVisible,
        bool safeguardingConcerns,
        bool sensitive,
        Action? onBeforeRequest = null
    )
    {
        ArgumentNullException.ThrowIfNull(graphQlClient);
        var noteTypesList = noteTypes.ToList();
        var nextCursor = default(string);
        while (true)
        {
            onBeforeRequest?.Invoke();
            var responseObject = await graphQlClient.GetChildNotes(
                    childId, noteTypesList, limit, parentVisible, safeguardingConcerns, sensitive, nextCursor
                ) ?? throw new InvalidOperationException();
            yield return responseObject;
            nextCursor = responseObject.Data.ChildNotes.Next;
            if (nextCursor is null)
            {
                yield break;
            }
        }
    }

}
