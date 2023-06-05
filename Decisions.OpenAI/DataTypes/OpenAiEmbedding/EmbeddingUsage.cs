using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    public class EmbeddingUsage
    {
        [DataMember]
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }
        
        [DataMember]
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}