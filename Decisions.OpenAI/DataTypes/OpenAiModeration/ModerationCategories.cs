using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    public class ModerationCategories
    {
        [DataMember] [JsonProperty("sexual")]
        public bool FlagSexual;
        
        [DataMember]
        [JsonProperty("hate")]
        public bool FlagHate;
        
        [DataMember]
        [JsonProperty("violence")]
        public bool FlagViolence;
        
        [DataMember]
        [JsonProperty("self-harm")]
        public bool FlagSelfHarm;
        
        [DataMember]
        [JsonProperty("sexual/minors")]
        public bool FlagMinors;
        
        [DataMember]
        [JsonProperty("hate/threatening")]
        public bool FlagThreatening;
        
        [DataMember]
        [JsonProperty("violence/graphic")]
        public bool FlagGraphic;
    }
}