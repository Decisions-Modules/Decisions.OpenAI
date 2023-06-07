using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFile
{
    [Writable]
    public class OpenAiFile
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }

        [WritableValue]
        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [WritableValue]
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [WritableValue]
        [JsonProperty("bytes")]
        public int Bytes { get; set; }

        [WritableValue]
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [WritableValue]
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}