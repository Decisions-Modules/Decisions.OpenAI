using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [DataContract]
    public class OpenAiEdit
    {
        [DataMember]
        public EditChoice Choice { get; set; }
        
        [DataMember]
        public EditResponse Response { get; set; }
        
        [DataMember]
        public Usage Usage { get; set; }
    }
}