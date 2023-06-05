using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    public class ModerationCategoryScores
    {
        [DataMember] [JsonProperty("sexual")]
        public decimal FlagSexual;
        
        [DataMember]
        [JsonProperty("hate")]
        public decimal FlagHate;
        
        [DataMember]
        [JsonProperty("violence")]
        public decimal FlagViolence;
        
        [DataMember]
        [JsonProperty("self-harm")]
        public decimal FlagSelfHarm;
        
        [DataMember]
        [JsonProperty("sexual/minors")]
        public decimal FlagMinors;
        
        [DataMember]
        [JsonProperty("hate/threatening")]
        public decimal FlagThreatening;
        
        [DataMember]
        [JsonProperty("violence/graphic")]
        public decimal FlagGraphic;
    }
}