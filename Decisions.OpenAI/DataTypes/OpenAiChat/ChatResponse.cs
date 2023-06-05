using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    public class ChatResponse
    {
        [DataMember]
        public string id { get; set; }
        
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public int created { get; set; }
        
        [DataMember]
        public List<ChatChoice> choices { get; set; }
        
        [DataMember]
        public Usage usage { get; set; }

        public static ChatResponse JsonDeserialize(string json)
        {
            try
            {
                ChatResponse text = JsonConvert.DeserializeObject<ChatResponse>(json);
                return text;
            }
            catch (Exception e)
            {
                throw new BusinessRuleException(e.Message);
            }
        }
    }
}