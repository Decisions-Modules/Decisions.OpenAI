using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [Writable]
    public class ListFineTuneEventsResponse
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("data")]
        public List<FineTuneEvent> Data { get; set; }
        
        public static ListFineTuneEventsResponse JsonDeserialize(string json)
        {
            try
            {
                ListFineTuneEventsResponse text = JsonConvert.DeserializeObject<ListFineTuneEventsResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}