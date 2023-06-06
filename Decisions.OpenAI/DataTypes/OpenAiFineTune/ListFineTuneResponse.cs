using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
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
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}