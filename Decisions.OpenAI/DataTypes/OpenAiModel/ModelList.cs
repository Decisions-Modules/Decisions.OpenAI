using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModel
{
    [DataContract]
    public class ModelList
    {
        [DataMember]
        [JsonProperty("data")]
        public List<OpenAiModel> Data { get; set; }
        
        [DataMember]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        public static ModelList JsonDeserialize(string json)
        {
            try
            {
                ModelList text = JsonConvert.DeserializeObject<ModelList>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}