using DecisionsFramework.Design.ConfigurationStorage.Attributes;

namespace Decisions.OpenAI.DataTypes.OpenAiChat
{
    [Writable]
    public class OpenAiMessage
    {
        [WritableValue]
        public ChatChoice Choice { get; set; }
        
        [WritableValue]
        public ChatMessage Message { get; set; }
        
        [WritableValue]
        public ChatResponse Response { get; set; }
        
        [WritableValue]
        public Usage Usage { get; set; }
    }
}