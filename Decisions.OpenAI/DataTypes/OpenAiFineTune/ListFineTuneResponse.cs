using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    [Writable]
    public class ListFineTuneResponse
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("data")]
        public List<FineTuneResponse> Data { get; set; }
        
        public static ListFineTuneResponse JsonDeserialize(string json)
        {
            try
            {
                ListFineTuneResponse text = JsonConvert.DeserializeObject<ListFineTuneResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}