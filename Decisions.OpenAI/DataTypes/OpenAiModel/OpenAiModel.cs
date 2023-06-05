using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModel
{
    [DataContract]
    public class OpenAiModel
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [DataMember]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [DataMember]
        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }
        
        [DataMember]
        [JsonProperty("permission")]
        public List<string> Permission { get; set; }
        
        public static OpenAiModel JsonDeserialize(string json)
        {
            try
            {
                OpenAiModel text = JsonConvert.DeserializeObject<OpenAiModel>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}