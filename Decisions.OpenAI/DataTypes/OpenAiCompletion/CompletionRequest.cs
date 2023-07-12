using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [DataContract]
    [Writable]
    public class CompletionRequest
    {
        [WritableValue]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [WritableValue]
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
        
        [WritableValue]
        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; }
        
        [WritableValue]
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        
        [WritableValue]
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
                throw new BusinessRuleException("There was a problem serializing request.", e);
            }
        }
    }
}