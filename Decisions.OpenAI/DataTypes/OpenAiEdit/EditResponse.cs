using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [DataContract]
    public class EditResponse
    {
        [DataMember]
        public string id { get; set; }
        
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public int created { get; set; }
        
        [DataMember]
        public List<EditChoice> choices { get; set; }
        
        [DataMember]
        public Usage usage { get; set; }
        
        public static EditResponse JsonDeserialize(string json)
        {
            try
            {
                EditResponse text = JsonConvert.DeserializeObject<EditResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}