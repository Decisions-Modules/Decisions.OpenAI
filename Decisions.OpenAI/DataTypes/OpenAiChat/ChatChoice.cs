using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [Writable]
    public class ChatChoice
    {
        [WritableValue]
        [JsonProperty("index")]
        public int Index { get; set; }
        
        [WritableValue]
        [JsonProperty("message")]
        public ChatMessage Message { get; set; }
        
        [WritableValue]
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }
}