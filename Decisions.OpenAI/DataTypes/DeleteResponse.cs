using System;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes
{
    [Writable]
    public class DeleteResponse
    {
        [WritableValue]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [WritableValue]
        [JsonProperty("object")]
        public string Object { get; set; }
        
        [WritableValue]
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
        
        public static DeleteResponse JsonDeserialize(string json)
        {
            try
            {
                DeleteResponse text = JsonConvert.DeserializeObject<DeleteResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}