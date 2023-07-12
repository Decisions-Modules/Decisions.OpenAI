using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    [Writable]
    public class ModerationResponse
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [WritableValue]
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
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}