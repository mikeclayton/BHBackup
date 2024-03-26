using BHBackup.Common.Helpers;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BHBackup.Client.Core;

public abstract class CoreApiClient
{

    public const string FamilyAppUri = "https://familyapp.brighthorizons.co.uk";


    internal CoreApiClient(HttpClient httpClient, CoreApiCredentials? apiCredentials)
    {
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.ApiCredentials = apiCredentials;
    }

    public HttpClient HttpClient
    {
        get;
    }

    private CoreApiCredentials? ApiCredentials
    {
        get;
    }

    protected static string JoinUrl(string prefix, string suffix)
    {
        return (prefix.EndsWith('/') || suffix.StartsWith('/'))
            ? $"{prefix}{suffix}"
            : $"{prefix}/{suffix}";
    }

    private HttpRequestMessage CreateHttpRequest(
        string requestUrl,
        Dictionary<string, string>? querystring,
        HttpMethod method,
        string? requestBody,
        string contentType
    )
    {
        // format the querystring
        var formattedQuerystring = (querystring == null)
            ? null
            : string.Join(
                "&",
                querystring.Select(
                    kvp => string.Concat(WebUtility.UrlEncode(kvp.Key), "=", WebUtility.UrlEncode(kvp.Value))
                )
            );
        // format the request url
        var formattedRequestUrl = requestUrl +
            (string.IsNullOrEmpty(formattedQuerystring) ? string.Empty : '?' + formattedQuerystring);
        // build the http request
        var request = new HttpRequestMessage(method, formattedRequestUrl)
        {
            Content = (requestBody == null)
                ? null
                : new StringContent(
                    requestBody,
                    Encoding.UTF8,
                    contentType
                )
        };
        if (this.ApiCredentials is not null)
        {
            request.Headers.Add("x-famly-accesstoken", this.ApiCredentials.AccessToken);
        }
        request.Headers.ConnectionClose = false;
        return request;
    }

    internal async Task<string> ExecuteTextRequestAsync(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, string? requestBody)
    {
        var request = this.CreateHttpRequest(
            requestUrl, querystring, method, requestBody, "application/text"
        );
        var response = await this.HttpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    internal async Task<TResponse> ExecuteJsonRequestAsync<TResponse>(string requestUrl, Dictionary<string, string>? querystring, HttpMethod method, object? requestBody, bool roundtrip)
    {

        var formattedRequestBody = (requestBody == null)
            ? null
            : JsonSerializer.Serialize(requestBody);

        var request = this.CreateHttpRequest(
            requestUrl, querystring, method, formattedRequestBody, "application/json"
        );

        // get the http response
        var response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        // parse the json
        var responseObject = JsonHelper.ConvertFromJson<TResponse>(responseContent, roundtrip);

        return responseObject;
    }

}
