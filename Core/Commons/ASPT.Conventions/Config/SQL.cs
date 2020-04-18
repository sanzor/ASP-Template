using System;
using System.Text.Json.Serialization;

namespace ASPT.Conventions {
    [Serializable]
    public  class SQL {
        [JsonPropertyName("conString")]
        public string Constring { get; set; }
    }
}