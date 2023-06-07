using System.Collections.Generic;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [Writable]
    public class EmbeddingData
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [WritableValue]
        [JsonProperty("embedding")]
        public List<float> Embedding { get; set; }
    }
}