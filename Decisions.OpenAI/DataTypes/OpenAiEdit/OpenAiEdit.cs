using DecisionsFramework.Design.ConfigurationStorage.Attributes;

namespace Decisions.OpenAI.DataTypes.OpenAiEdit
{
    [Writable]
    public class OpenAiEdit
    {
        [WritableValue]
        public EditChoice Choice { get; set; }
        
        [WritableValue]
        public EditResponse Response { get; set; }
        
        [WritableValue]
        public Usage Usage { get; set; }
    }
}