using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    [Writable]
    public class EmbeddingRequest
    {
        [WritableValue]
        [JsonProperty("input")]
        public string[] Input { get; set; }
        
        [WritableValue]
        [JsonProperty("model")]
        public string Model { get; set; }
        
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