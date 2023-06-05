using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [DataContract]
    public class EditRequest
    {
        [DataMember]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [DataMember]
        [JsonProperty("input")]
        public string Input { get; set; }
        
        [DataMember]
        [JsonProperty("instruction")]
        public string Instruction { get; set; }
        
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