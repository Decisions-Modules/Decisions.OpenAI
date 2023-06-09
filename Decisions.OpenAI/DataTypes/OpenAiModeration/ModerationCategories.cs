using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    [Writable]
    public class ModerationCategories
    {
        [WritableValue] [JsonProperty("sexual")]
        public bool FlagSexual;
        
        [WritableValue]
        [JsonProperty("hate")]
        public bool FlagHate;
        
        [WritableValue]
        [JsonProperty("violence")]
        public bool FlagViolence;
        
        [WritableValue]
        [JsonProperty("self-harm")]
        public bool FlagSelfHarm;
        
        [WritableValue]
        [JsonProperty("sexual/minors")]
        public bool FlagMinors;
        
        [WritableValue]
        [JsonProperty("hate/threatening")]
        public bool FlagHateThreat;
        
        [WritableValue]
        [JsonProperty("violence/graphic")]
        public bool FlagGraphic;
        
        [WritableValue]
        [JsonProperty("harassment")]
        public bool FlagHarassment;
        
        [WritableValue]
        [JsonProperty("harassment/threatening")]
        public bool FlagHarassmentThreat;
        
        [WritableValue]
        [JsonProperty("self-harm/intent")]
        public bool FlagSelfHarmIntent;
        
        [WritableValue]
        [JsonProperty("self-harm/instruction")]
        public bool FlagSelfHarmInstruct;
    }
}