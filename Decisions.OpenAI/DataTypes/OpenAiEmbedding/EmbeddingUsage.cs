using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    [Writable]
    public class EmbeddingUsage
    {
        [WritableValue]
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }
        
        [WritableValue]
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}