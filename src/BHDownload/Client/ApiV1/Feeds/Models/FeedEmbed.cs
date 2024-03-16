using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

[JsonConverter(typeof(FeedEmbedConverter))]
internal class FeedEmbed
{

}
