using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Notex.Infrastructure.EventSourcing;

public class EventSourcedSetting
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Objects,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None,
        Converters = new JsonConverter[] { new StringEnumConverter() },
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
    };
}