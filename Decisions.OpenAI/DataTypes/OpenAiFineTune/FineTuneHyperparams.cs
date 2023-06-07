using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [Writable]
    public class FineTuneHyperparams
    {
        [WritableValue]
        [JsonProperty("batch_size")]
        public int? BatchSize { get; set; }
        
        [WritableValue]
        [JsonProperty("learning_rate_multiplier")]
        public double? LearningRateMultiplier { get; set; }
        
        [WritableValue]
        [JsonProperty("n_epochs")]
        public int? NumberOfEpochs { get; set; }
        
        [WritableValue]
        [JsonProperty("prompt_loss_weight")]
        public double? PromptLossWeight { get; set; }
    }
}