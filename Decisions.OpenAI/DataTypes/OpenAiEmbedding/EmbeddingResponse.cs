using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEmbedding
{
    [DataContract]
    public class EmbeddingResponse
    {
        [DataMember]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [DataMember]
        [JsonProperty("data")]
        public List<EmbeddingData> Data { get; set; }
        
        [DataMember]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [DataMember]
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
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}