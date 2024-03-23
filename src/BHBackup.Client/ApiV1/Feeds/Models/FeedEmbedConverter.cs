using System.Text.Json;
using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

public sealed class FeedEmbedConverter : JsonConverter<FeedEmbed>
{

    public override bool CanConvert(Type typeToConvert) =>
        typeof(FeedEmbed).IsAssignableFrom(typeToConvert);

    public override FeedEmbed Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException();
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        // struct assignment *copies* the value so we can use 'reader' as-is later
        var readerCopy = reader;
        var document = JsonDocument.ParseValue(ref readerCopy);

        var typeName = document.RootElement.GetProperty("type").GetString()
            ?? throw new JsonException();
        var map = new Dictionary<string, Type>
        {
            ["Daycare.Event"] = typeof(FeedEmbedDaycareEvent),
            ["Observation"] = typeof(FeedEmbedObservation),
        };
        if (!map.TryGetValue(typeName, out var derivedType))
        {
            throw new JsonException();
        }

        var derivedResult = (FeedEmbed)(JsonSerializer.Deserialize(ref reader, derivedType, options)
            ?? throw new InvalidOperationException());

        return derivedResult;

    }

    public override void Write(
        Utf8JsonWriter writer, FeedEmbed value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case FeedEmbedDaycareEvent feedEmbedDaycareEvent:
                JsonSerializer.Serialize(writer, feedEmbedDaycareEvent, options);
                break;
            case FeedEmbedObservation feedEmbedObservation:
                JsonSerializer.Serialize(writer, feedEmbedObservation, options);
                break;
            default:
                throw new JsonException();
        }
    }

}
