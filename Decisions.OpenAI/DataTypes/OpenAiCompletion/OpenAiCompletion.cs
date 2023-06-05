using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [DataContract]
    public class OpenAiCompletion
    {
        [DataMember]
        public CompletionChoice Choice { get; set; }
        
        [DataMember]
        public CompletionResponse Response { get; set; }
        
        [DataMember]
        public Usage Usage { get; set; }
    }
}