using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModel
{
    [Writable]
    public class ModelPermission
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_create_engine")]
        public bool AllowCreateEngine { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_sampling")]
        public bool AllowSampling { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_logprobs")]
        public bool AllowLogprobs { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_search_indices")]
        public bool AllowSearchIndices { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_view")]
        public bool AllowView { get; set; }
        
        [WritableValue]
        [JsonProperty("allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }
        
        [WritableValue]
        [JsonProperty("organization")]
        public string Organization { get; set; }
        
        [WritableValue]
        [JsonProperty("group")]
        public string Group { get; set; }
        
        [WritableValue]
        [JsonProperty("is_blocking")]
        public bool IsBlocking { get; set; }
    }
}