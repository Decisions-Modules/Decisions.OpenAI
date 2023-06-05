using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    public class FineTuneResponse
    {
        [DataMember]
        public string id { get; set; }
        
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public string model { get; set; }
        
        [DataMember]
        public int created_at { get; set; }
        
        [DataMember]
        public List<FineTuneEvent> events { get; set; }
        
        [DataMember]
        public string fine_tuned_model { get; set; }
        
        [DataMember]
        public string organization_id { get; set; }
        
        [DataMember]
        public List<OpenAiFile.OpenAiFile> result_files { get; set; }
        
        [DataMember]
        public string status { get; set; }
        
        [DataMember]
        public List<OpenAiFile.OpenAiFile> validation_files { get; set; }
        
        [DataMember]
        public List<OpenAiFile.OpenAiFile> training_files { get; set; }
        
        [DataMember]
        public FineTuneHyperparams hyperparams { get; set; }
        
        [DataMember]
        public int updated_at { get; set; }

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