using BHBackup.Client.GraphQl.ChildNotes.Api;

namespace BHBackup.Client.GraphQl.ChildNotes;

internal static class ChildNotesExtensions
{

    public static async IAsyncEnumerable<GetChildNotesResponse> PaginateChildNotes(
        this GraphQlClient graphQlClient,
        string childId,
        IEnumerable<string> noteTypes,
        int limit,
        bool parentVisible,
        bool safeguardingConcerns,
        bool sensitive,
        Action? onBeforeReadPage = null
    )
    {

        ArgumentNullException.ThrowIfNull(graphQlClient);

        var noteTypesList = noteTypes.ToList();

        var nextCursor = default(string);
        while (true)
        {

            onBeforeReadPage?.Invoke();
            var responseObject = await graphQlClient.GetChildNotes(
                    childId, noteTypesList, limit, parentVisible, safeguardingConcerns, sensitive, nextCursor
                )
                ?? throw new InvalidOperationException();
            yield return responseObject;

            nextCursor = responseObject.Data.ChildNotes.Next;
            if (nextCursor is null)
            {
                break;
            }

        }

    }

}
