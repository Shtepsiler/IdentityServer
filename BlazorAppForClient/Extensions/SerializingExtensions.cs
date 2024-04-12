using System.Text.Json;

namespace BlazorAppForClient.Extensions
{
    public static class SerializingExtensions
    {
        public static string Serialize<T>(this T obj) =>
            JsonSerializer.Serialize(obj,JsonSerializerOptions.Default );

        public static TOut Deserialize<TOut>(this string obj) =>
            JsonSerializer.Deserialize<TOut>(obj, JsonSerializerOptions.Default);
    }
}
