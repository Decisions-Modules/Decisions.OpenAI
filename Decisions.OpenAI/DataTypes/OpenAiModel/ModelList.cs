using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModel
{
    [DataContract]
    [Writable]
    public class ModelList
    {
        [WritableValue]
        [JsonProperty("data")]
        public List<OpenAIModel> Data { get; set; }
        
        [WritableValue]
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