using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiFile
{
    [DataContract]
    public class OpenAiFileContainer
    {
        [DataMember]
        public string Object
        {
            get;
            set;
        }

        [DataMember]
        public OpenAiFile[] Data
        {
            get;
            set;
        }
    }
}