using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [DataContract]
    public class CompletionRequest
    {
        [DataMember]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [DataMember]
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
        
        [DataMember]
        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; }
        
        [DataMember]
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        
        [DataMember]
        [JsonProperty("n")]
        public int N { get; set; }

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