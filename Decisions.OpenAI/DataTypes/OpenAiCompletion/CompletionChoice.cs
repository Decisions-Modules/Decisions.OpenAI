using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [Writable]
    public class CompletionChoice
    {
        [WritableValue]
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [WritableValue]
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [WritableValue]
        [JsonProperty("logprobs")]
        public int? Logprobs { get; set; }
        
        [WritableValue]
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }
}