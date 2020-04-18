using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ASPT.Conventions {
    [Serializable]
    public class Config {
        [JsonPropertyName("serverUrl")]
        public string ServerUrl { get; set; }
        [JsonPropertyName("swagger")]
        public Swagger Swagger { get; set; }
        [JsonPropertyName("sql")]
        public SQL Sql { get; set; }
    }
}
