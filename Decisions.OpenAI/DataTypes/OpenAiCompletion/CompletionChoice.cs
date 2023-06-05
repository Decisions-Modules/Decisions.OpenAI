using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [DataContract]
    public class CompletionChoice
    {
        [DataMember]
        public string text { get; set; }
        
        [DataMember]
        public int index { get; set; }
        
        [DataMember]
        public int? logprobs { get; set; }
        
        [DataMember]
        public string finish_reason { get; set; }
    }
}