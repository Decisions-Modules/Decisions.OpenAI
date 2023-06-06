using System;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
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
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}