using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CsvUploader.Web.Utilities;

[AttributeUsage(AttributeTargets.Property)]
public class SourceMapAttribute : Attribute
{
    public string[] Names { get; }
    public SourceMapAttribute(params string[] names) => Names = names;
}

public class SourceMapConverter<T> : JsonConverter<T> where T : new()
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var obj = new T();
        var map = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Prop: p, Attr: p.GetCustomAttribute<SourceMapAttribute>()))
            .SelectMany(x => (x.Attr?.Names ?? new[] { x.Prop.Name })
                .Append(x.Prop.Name)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(name => (Name: name, x.Prop)))
            .ToDictionary(x => x.Name, x => x.Prop, StringComparer.OrdinalIgnoreCase);

        using var doc = JsonDocument.ParseValue(ref reader);
        foreach (var jp in doc.RootElement.EnumerateObject())
        {
            if (map.TryGetValue(jp.Name, out var prop))
            {
                var val = JsonSerializer.Deserialize(jp.Value.GetRawText(), prop.PropertyType, options);
                prop.SetValue(obj, val);
            }
        }

        return obj;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead)
            {
                continue;
            }

            var propertyName = prop.GetCustomAttribute<SourceMapAttribute>()?.Names?.FirstOrDefault() ?? prop.Name;
            writer.WritePropertyName(propertyName);
            var propertyValue = prop.GetValue(value);
            JsonSerializer.Serialize(writer, propertyValue, prop.PropertyType, options);
        }

        writer.WriteEndObject();
    }
}