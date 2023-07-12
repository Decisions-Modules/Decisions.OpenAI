using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes
{
    [DataContract]
    [Writable]
    public class Usage
    {
        [WritableValue]
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }
        
        [WritableValue]
        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }
        
        [WritableValue]
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}