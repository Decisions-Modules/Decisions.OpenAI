using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    public class ListFineTuneEventsResponse
    {
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public List<FineTuneEvent> data { get; set; }
        
        public static ListFineTuneEventsResponse JsonDeserialize(string json)
        {
            try
            {
                ListFineTuneEventsResponse text = JsonConvert.DeserializeObject<ListFineTuneEventsResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}