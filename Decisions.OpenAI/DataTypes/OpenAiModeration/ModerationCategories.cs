using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
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
        public bool FlagThreatening;
        
        [WritableValue]
        [JsonProperty("violence/graphic")]
        public bool FlagGraphic;
    }
}