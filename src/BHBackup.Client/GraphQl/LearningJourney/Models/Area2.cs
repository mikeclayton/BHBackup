using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

/// <summary>
/// Same as Area1, but with different json serialization order
/// </summary>
public sealed class Area2
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    /// <summary>
    /// Known values:
    ///   * "CL"   - "Communication and Language"
    ///   * "ELAN" - "Exploring and Learning about my World"
    ///   * "FF"   - "Feelings and Friendships"
    ///   * "L"    - "Literacy"
    ///   * "LA"   - "Listening and attention"
    ///   * "M"    - "Mathematics"
    ///   * "N"    - "Numbers"
    ///   * "M&H"  - "Moving and Handling"
    ///   * "MCS"  - "Health and self-care"
    ///   * "MFB"  - "Managing feelings and behaviour"
    ///   * "MR"   - "Making relationships"
    ///   * "R"    - "Reading"
    ///   * "S"    - "Speaking"
    ///   * "SSM"  - "Shape, space and measures"
    ///   * "PD"   - "Physical Development"
    ///   * "PC"   - "People and Communities"
    ///   * "PSED" - "Personal, social and emotional"
    ///   * "STI"  - "Sharing Thoughts and Ideas"
    ///   * "SC"   - "Self-confidence and self-awareness"
    ///   * "TC"   - "Thinking Creatively"
    ///   * "TLS"  - "Technical and Life Skills"
    ///   * "U"    - "Understanding"
    ///   * "UW"   - "Understanding the world"
    ///   * "W"    - "Writing"
    /// </summary>
    [JsonPropertyName("abbr")]
    public string Abbreviation
    {
        get;
        init;
    }

    [JsonPropertyName("title")]
    public string Title
    {
        get;
        init;
    }

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

}
