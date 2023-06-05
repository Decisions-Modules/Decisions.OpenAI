using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes
{
    [DataContract]
    public class Usage
    {
        [DataMember]
        public int prompt_tokens { get; set; }
        
        [DataMember]
        public int completion_tokens { get; set; }
        
        [DataMember]
        public int total_tokens { get; set; }
    }
}