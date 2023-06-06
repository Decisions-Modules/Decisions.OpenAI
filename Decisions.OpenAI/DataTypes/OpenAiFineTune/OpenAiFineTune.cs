using DecisionsFramework.Design.ConfigurationStorage.Attributes;

namespace Decisions.OpenAI.DataTypes.OpenAiFineTune
{
    [Writable]
    public class OpenAiFineTune
    {
        [WritableValue]
        public FineTuneEvent Event { get; set; }
        
        [WritableValue]
        public FineTuneHyperparams Hyperparams { get; set; }
        
        [WritableValue]
        public FineTuneResponse Response { get; set; }
    }
}