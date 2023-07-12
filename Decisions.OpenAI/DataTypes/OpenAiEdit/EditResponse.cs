using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [DataContract]
    [Writable]
    public class EditResponse
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [WritableValue]
        [JsonProperty("choices")]
        public List<EditChoice> Choices { get; set; }
        
        [WritableValue]
        [JsonProperty("usage")]
        public Usage Usage { get; set; }
        
        public static EditResponse JsonDeserialize(string json)
        {
            try
            {
                EditResponse text = JsonConvert.DeserializeObject<EditResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}