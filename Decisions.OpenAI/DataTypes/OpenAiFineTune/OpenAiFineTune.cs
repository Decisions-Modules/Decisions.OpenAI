using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    public class OpenAiFineTune
    {
        [DataMember]
        public FineTuneEvent Event { get; set; }
        
        [DataMember]
        public FineTuneHyperparams Hyperparams { get; set; }
        
        [DataMember]
        public FineTuneResponse Response { get; set; }
    }
}