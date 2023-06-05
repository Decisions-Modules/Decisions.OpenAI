using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [DataContract]
    public class EditChoice
    {
        [DataMember]
        public string text { get; set; }
        
        [DataMember]
        public int index { get; set; }
    }
}