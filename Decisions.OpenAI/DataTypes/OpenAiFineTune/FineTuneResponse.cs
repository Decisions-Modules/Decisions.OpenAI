using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [Writable]
    public class FineTuneResponse
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
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        
        [WritableValue]
        [JsonProperty("events")]
        public List<FineTuneEvent> Events { get; set; }
        
        [WritableValue]
        [JsonProperty("fine_tuned_model")]
        public string FineTunedModel { get; set; }
        
        [WritableValue]
        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }
        
        [WritableValue]
        [JsonProperty("result_files")]
        public List<OpenAiFile.OpenAiFile> ResultFiles { get; set; }
        
        [WritableValue]
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [WritableValue]
        [JsonProperty("validation_files")]
        public List<OpenAiFile.OpenAiFile> ValidationFiles { get; set; }
        
        [WritableValue]
        [JsonProperty("training_files")]
        public List<OpenAiFile.OpenAiFile> TrainingFiles { get; set; }
        
        [WritableValue]
        [JsonProperty("hyperparams")]
        public FineTuneHyperparams Hyperparams { get; set; }
        
        [WritableValue]
        [JsonProperty("updated_at")]
        public int UpdatedAt { get; set; }

        public static FineTuneResponse JsonDeserialize(string json)
        {
            try
            {
                FineTuneResponse text = JsonConvert.DeserializeObject<FineTuneResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}