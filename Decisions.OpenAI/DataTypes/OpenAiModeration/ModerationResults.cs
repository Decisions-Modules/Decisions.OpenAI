using System.Runtime.Serialization;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    [Writable]
    public class ModerationResults
    {
        [WritableValue]
        [JsonProperty("flagged")]
        public bool Flagged { get; set; }
        
        [WritableValue]
        [JsonProperty("categories")]
        public ModerationCategories Categories { get; set; }
        
        [WritableValue]
        [JsonProperty("category_scores")]
        public ModerationCategoryScores CategoryScores { get; set; }
    }
}