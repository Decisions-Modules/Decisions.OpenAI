using System.Runtime.Serialization;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [DataContract]
    public class FineTuneHyperparams
    {
        [DataMember]
        public int? batch_size { get; set; }
        
        [DataMember]
        public double? learning_rate_multiplier { get; set; }
        
        [DataMember]
        public int? n_epochs { get; set; }
        
        [DataMember]
        public double? prompt_loss_weight { get; set; }
    }
}