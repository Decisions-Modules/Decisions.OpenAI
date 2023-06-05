using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [DataContract]
    public class CompletionResponse
    {
        [DataMember]
        public string id { get; set; }
        
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public string model { get; set; }
        
        [DataMember]
        public int created { get; set; }
        
        [DataMember]
        public List<CompletionChoice> choices { get; set; }
        
        [DataMember]
        public Usage usage { get; set; }
        
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