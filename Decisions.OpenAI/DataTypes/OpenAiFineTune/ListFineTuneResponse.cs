using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    public class ListFineTuneResponse
    {
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public List<FineTuneResponse> data { get; set; }
        
        public static ListFineTuneResponse JsonDeserialize(string json)
        {
            try
            {
                ListFineTuneResponse text = JsonConvert.DeserializeObject<ListFineTuneResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}