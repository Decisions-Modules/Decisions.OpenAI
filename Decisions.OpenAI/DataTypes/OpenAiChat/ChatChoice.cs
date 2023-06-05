using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    public class ChatChoice
    {
        [DataMember]
        public int index { get; set; }
        
        [DataMember]
        public ChatMessage message { get; set; }
        
        [DataMember]
        public string finish_reason { get; set; }
    }
}