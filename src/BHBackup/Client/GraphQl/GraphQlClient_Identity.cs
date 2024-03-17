﻿using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Client.GraphQl.Identity.Queries;
using BHBackup.Helpers;
using System.Reflection;

namespace BHBackup.Client.GraphQl;

internal sealed partial class GraphQlClient
{

    public async Task<GetCurrentContextResponse> GetCurrentContext()
    {

        var requestUri = $"/graphql?{nameof(GetCurrentContext)}";
        var requestBody = new
        {
            operationName = nameof(GetCurrentContext),
            variables = new { },
            query = EmbeddedResourceHelper.ReadEmbeddedResourceText(
                Assembly.GetExecutingAssembly(),
                $"{typeof(EmbeddedResources).Namespace}.{nameof(GetCurrentContext)}.graphql"
            )
        };

        return await this.ExecuteGraphQlRequest<GetCurrentContextResponse>(
            requestUrl: $"/graphql?{nameof(GetCurrentContext)}",
            querystring: null,
            method: HttpMethod.Post,
            requestBody: requestBody,
            roundtrip: true
        );

    }

}