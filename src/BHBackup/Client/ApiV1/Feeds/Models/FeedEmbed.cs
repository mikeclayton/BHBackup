using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

[JsonConverter(typeof(FeedEmbedConverter))]
internal class FeedEmbed
{

}
