using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Decisions.OpenAI.DataTypes.OpenAiModeration
{
    [DataContract]
    public class ModerationResults
    {
        [DataMember]
        [JsonProperty("flagged")]
        public bool Flagged { get; set; }
        
        [DataMember]
        [JsonProperty("categories")]
        public ModerationCategories Categories { get; set; }
        
        [DataMember]
        [JsonProperty("category_scores")]
        public ModerationCategoryScores CategoryScores { get; set; }
    }
}