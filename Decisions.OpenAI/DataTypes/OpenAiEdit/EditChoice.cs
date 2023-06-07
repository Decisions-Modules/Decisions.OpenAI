using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [Writable]
    public class EditChoice
    {
        [WritableValue]
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [WritableValue]
        [JsonProperty("index")]
        public int Index { get; set; }
    }
}