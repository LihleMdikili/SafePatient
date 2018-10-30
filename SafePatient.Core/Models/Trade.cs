using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SafePatient.Core.Models {
    public class Trade {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Full Name")]
        public string FullName { get; set; }
    }
}
