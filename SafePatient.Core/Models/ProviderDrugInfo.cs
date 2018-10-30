using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SafePatient.Core.Models {

    public partial class ProviderDrugInfo {
        [JsonProperty("DATA")]
        public Dictionary<string, object[]> Data { get; set; }

        [JsonProperty("MESSAGE")]
        public Message Message { get; set; }
    }

    public partial class Message {
        [JsonProperty("NoData")]
        public string NoData { get; set; }
    }

    public partial class ProviderDrugInfo {
        public static ProviderDrugInfo FromJson(string json) => JsonConvert.DeserializeObject<ProviderDrugInfo>(json, Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson(this ProviderDrugInfo self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
