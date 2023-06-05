using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    public class EmbeddingData
    {
        [DataMember]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [DataMember]
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [DataMember]
        [JsonProperty("embedding")]
        public List<float> Embedding { get; set; }
    }
}