using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    public class ChatRequest
    {
        [DataMember]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [DataMember]
        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; }

        public string JsonSerialize()
        {
            try
            {
                string request = JsonConvert.SerializeObject(this);
                return request;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
    
}