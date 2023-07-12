using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFile
{
    [DataContract]
    [Writable]
    public class OpenAiFileContainer
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }

        [WritableValue]
        [JsonProperty("data")]
        public OpenAiFile[] Data { get; set; }
    }
}