using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    public class ModerationResponse
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [DataMember]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [DataMember]
        [JsonProperty("results")]
        public List<ModerationResults> Results { get; set; }
        
        public static ModerationResponse JsonDeserialize(string json)
        {
            try
            {
                ModerationResponse text = JsonConvert.DeserializeObject<ModerationResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}