using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    [Writable]
    public class ChatMessage
    {
        [WritableValue]
        [JsonProperty("role")]
        public string Role { get; set; }
        
        [WritableValue]
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}