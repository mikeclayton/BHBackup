using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

[JsonConverter(typeof(FeedEmbedConverter))]
public class FeedEmbed
{

}
