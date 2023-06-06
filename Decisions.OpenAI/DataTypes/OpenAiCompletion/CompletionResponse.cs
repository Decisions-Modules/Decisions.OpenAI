using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [Writable]
    public class CompletionResponse
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [WritableValue]
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [WritableValue]
        [JsonProperty("choices")]
        public List<CompletionChoice> Choices { get; set; }
        
        [WritableValue]
        [JsonProperty("usage")]
        public Usage Usage { get; set; }
        
        public static CompletionResponse JsonDeserialize(string json)
        {
            try
            {
                CompletionResponse text = JsonConvert.DeserializeObject<CompletionResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}