using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    public class OpenAiMessage
    {
        [DataMember]
        public ChatChoice Choice { get; set; }
        
        [DataMember]
        public ChatMessage Message { get; set; }
        
        [DataMember]
        public ChatResponse Response { get; set; }
        
        [DataMember]
        public Usage Usage { get; set; }
    }
}