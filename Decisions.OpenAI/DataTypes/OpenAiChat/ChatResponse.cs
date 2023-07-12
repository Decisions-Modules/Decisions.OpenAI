using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    [Writable]
    public class ChatResponse
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [WritableValue]
        [JsonProperty("choices")]
        public List<ChatChoice> Choices { get; set; }
        
        [WritableValue]
        [JsonProperty("usage")]
        public Usage Usage { get; set; }

        public static ChatResponse JsonDeserialize(string json)
        {
            try
            {
                ChatResponse text = JsonConvert.DeserializeObject<ChatResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}