using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModel
{
    [Writable]
    public class OpenAiModel
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [WritableValue]
        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }
        
        [WritableValue]
        [JsonProperty("permission")]
        public List<ModelPermission> Permission { get; set; }
        
        [WritableValue]
        [JsonProperty("root")]
        public string Root { get; set; }
        
        [WritableValue]
        [JsonProperty("parent")]
        public string Parent { get; set; }
        
        public static OpenAiModel JsonDeserialize(string json)
        {
            try
            {
                OpenAiModel text = JsonConvert.DeserializeObject<OpenAiModel>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}