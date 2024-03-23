using System.Text.Json;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Authentication;

public sealed class AuthenticateWithPasswordConverter : JsonConverter<AuthenticateWithPassword>
{

    public override bool CanConvert(Type typeToConvert) =>
        typeof(AuthenticateWithPassword).IsAssignableFrom(typeToConvert);

    public override AuthenticateWithPassword Read(
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

        var status = document.RootElement.GetProperty("status").GetString()
            ?? throw new JsonException();
        var map = new Dictionary<string, Type>
        {
            ["Succeeded"] = typeof(AuthenticationSucceeded),
            ["FailedInvalidPassword"] = typeof(FailedInvalidPassword),
        };
        if (!map.TryGetValue(status, out var derivedType))
        {
            throw new JsonException();
        }

        var derivedResult = (AuthenticateWithPassword)(JsonSerializer.Deserialize(ref reader, derivedType, options)
            ?? throw new InvalidOperationException());

        return derivedResult;

    }

    public override void Write(
        Utf8JsonWriter writer, AuthenticateWithPassword value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case AuthenticationSucceeded authenticationSucceeded:
                JsonSerializer.Serialize(writer, authenticationSucceeded, options);
                break;
            case FailedInvalidPassword failedInvalidPassword:
                JsonSerializer.Serialize(writer, failedInvalidPassword, options);
                break;
            default:
                throw new JsonException();
        }
    }

}
