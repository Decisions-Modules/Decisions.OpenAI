using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    [Writable]
    public class FineTuneRequest
    {
        [WritableValue]
        [JsonProperty("training_file")]
        public string TrainingFile { get; set; }
        
        public string JsonSerialize()
        {
            try
            {
                string request = JsonConvert.SerializeObject(this);
                return request;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException("There was a problem serializing request.", e);
            }
        }
    }
}