using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    [Writable]
    public class ModerationCategoryScores
    {
        [WritableValue] [JsonProperty("sexual")]
        public decimal FlagSexual;
        
        [WritableValue]
        [JsonProperty("hate")]
        public decimal FlagHate;
        
        [WritableValue]
        [JsonProperty("violence")]
        public decimal FlagViolence;
        
        [WritableValue]
        [JsonProperty("self-harm")]
        public decimal FlagSelfHarm;
        
        [WritableValue]
        [JsonProperty("sexual/minors")]
        public decimal FlagMinors;
        
        [WritableValue]
        [JsonProperty("hate/threatening")]
        public decimal FlagThreatening;
        
        [WritableValue]
        [JsonProperty("violence/graphic")]
        public decimal FlagGraphic;
        
        [WritableValue]
        [JsonProperty("harassment")]
        public decimal FlagHarassment;
        
        [WritableValue]
        [JsonProperty("harassment/threatening")]
        public decimal FlagHarassmentThreat;
        
        [WritableValue]
        [JsonProperty("self-harm/intent")]
        public decimal FlagSelfHarmIntent;
        
        [WritableValue]
        [JsonProperty("self-harm/instruction")]
        public decimal FlagSelfHarmInstruct;
    }
}