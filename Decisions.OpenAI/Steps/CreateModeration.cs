using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiModeration;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Moderation", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, "flow step images|openai.svg")]
    public class CreateModeration : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string INPUT = "Input";
        private const string OPENAI_MODERATION_RESPONSE = "OpenAiModeration";
        
        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }

        public ResultData Run(StepStartData data)
        {
            string input = data[INPUT] as string;
            
            string extension = "moderations";
            ModerationRequest request = new ModerationRequest();

            request.Input = input;
            string messageRequest = request.JsonSerialize();

            ModerationResponse moderationResponse = ModerationResponse.JsonDeserialize(OpenAiRest.OpenAiPost(messageRequest, extension, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_MODERATION_RESPONSE, moderationResponse);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(ModerationResponse), OPENAI_MODERATION_RESPONSE))
                };
            }
        }

        public DataDescription[] InputData
        {
            get
            {
                List<DataDescription> input = new List<DataDescription>();
                input.AddRange(new[]
                {
                    new DataDescription(typeof(string), INPUT)
                });
            
                return input.ToArray();
            }
        }
    }
}