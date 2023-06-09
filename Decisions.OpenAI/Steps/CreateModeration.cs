using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiModeration;
using Decisions.OpenAI.Settings;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Moderation", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class CreateModeration : ISyncStep, IDataConsumer
    {
        private const string EXTENSION = "moderations";
        private const string PATH_DONE = "Done";

        private const string INPUT = "Input";
        private const string OPENAI_MODERATION_RESPONSE = "OpenAI Moderation";

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

            if (string.IsNullOrEmpty(input))
            {
                throw new BusinessRuleException($"{INPUT} cannot be null or empty.");
            }
            
            ModerationRequest request = new ModerationRequest();

            request.Input = input;
            string messageRequest = request.JsonSerialize();

            ModerationResponse moderationResponse = ModerationResponse.JsonDeserialize(OpenAiRest.OpenAiPost(messageRequest, EXTENSION, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_MODERATION_RESPONSE, moderationResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(ModerationResponse), OPENAI_MODERATION_RESPONSE)
                    {
                        DisplayName = OPENAI_MODERATION_RESPONSE
                    })
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