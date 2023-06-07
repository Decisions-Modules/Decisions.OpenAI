using DecisionsFramework.Design.ConfigurationStorage.Attributes;

namespace Decisions.OpenAI.DataTypes.OpenAiCompletion
{
    [Writable]
    public class OpenAiCompletion
    {
        [WritableValue]
        public CompletionChoice Choice { get; set; }
        
        [WritableValue]
        public CompletionResponse Response { get; set; }
        
        [WritableValue]
        public Usage Usage { get; set; }
    }
}