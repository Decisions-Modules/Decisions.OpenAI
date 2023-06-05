using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFile
{
    [DataContract]
    public class OpenAiFile
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        [DataMember]
        [JsonProperty("purpose")]
        public string Purpose
        {
            get;
            set;
        }

        [DataMember]
        [JsonProperty("filename")]
        public string FileName
        {
            get;
            set;
        }

        [DataMember]
        [JsonProperty("bytes")]
        public int bytes
        {
            get;
            set;
        }

        [DataMember]
        [JsonProperty("created_at")]
        public int CreatedAt
        {
            get;
            set;
        }

        [DataMember]
        [JsonProperty("status")]
        public string Status
        {
            get;
            set;
        }
    }
}