using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    public class FineTuneEvent
    {
        [DataMember]
        public string @object { get; set; }
        
        [DataMember]
        public int created_at { get; set; }
        
        [DataMember]
        public string level { get; set; }
        
        [DataMember]
        public string message { get; set; }
    }
}