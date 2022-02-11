using System.Text.Json;

namespace SmartNote.Core.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public static T FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, JsonOptions);

    public static string ToJson<T>(this T obj) =>
        JsonSerializer.Serialize<T>(obj, JsonOptions);
}