using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    public class EmbeddingRequest
    {
        [DataMember]
        [JsonProperty("input")]
        public string[] Input { get; set; }
        
        [DataMember]
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