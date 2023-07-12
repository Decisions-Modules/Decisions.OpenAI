using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    [Writable]
    public class EmbeddingResponse
    {
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("data")]
        public List<EmbeddingData> Data { get; set; }
        
        [WritableValue]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [WritableValue]
        [JsonProperty("usage")]
        public EmbeddingUsage Usage { get; set; }
        
        public static EmbeddingResponse JsonDeserialize(string json)
        {
            try
            {
                EmbeddingResponse text = JsonConvert.DeserializeObject<EmbeddingResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem deserializing response.", e);
            }
        }
    }
}