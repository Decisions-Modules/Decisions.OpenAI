using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        public string role { get; set; }
        
        [DataMember]
        public string content { get; set; }
    }
}