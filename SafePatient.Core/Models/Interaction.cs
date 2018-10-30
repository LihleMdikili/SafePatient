using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SafePatient.Core.Models {

    public partial class Interaction {
        [JsonProperty("DATA")]
        public Datum[] Data { get; set; }

        [JsonProperty("MESSAGE")]
        public Message Message { get; set; }
    }

    public partial class Datum {
        [JsonProperty("TradeA")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TradeA { get; set; }

        [JsonProperty("MedicineA")]
        public string MedicineA { get; set; }

        [JsonProperty("TradeB")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TradeB { get; set; }

        [JsonProperty("MedicineB")]
        public string MedicineB { get; set; }

        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("SeverityRank")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SeverityRank { get; set; }

        [JsonProperty("Details")]
        public Details Details { get; set; }
    }

    public partial class Details {
        [JsonProperty("Detail1")]
        public Detail1 Detail1 { get; set; }
    }

    public partial class Detail1 {
        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ReferenceID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ReferenceId { get; set; }
    }

    public partial class Interaction {
        public static Interaction FromJson(string json) => JsonConvert.DeserializeObject<Interaction>(json, Converter.Settings);
    }

    internal class ParseStringConverter : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Int64.TryParse(value, out long l)) {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
            if (untypedValue == null) {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
