using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    [Writable]
    public class FineTuneEvent
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        
        [WritableValue]
        [JsonProperty("level")]
        public string Level { get; set; }
        
        [WritableValue]
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}