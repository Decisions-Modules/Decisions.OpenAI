using System;
using System.Runtime.Serialization;
using DecisionsFramework;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes
{
    [DataContract]
    public class DeleteResponse
    {
        [DataMember]
        public string id { get; set; }
        
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public bool deleted { get; set; }
        
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