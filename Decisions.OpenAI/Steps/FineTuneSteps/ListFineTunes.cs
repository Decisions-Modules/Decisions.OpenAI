using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiFineTune;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps.FineTuneSteps
{
    [Writable]
    [AutoRegisterStep("List Fine Tunes", "Integration/OpenAI/Fine-Tune")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class ListFineTunes : ISyncStep, IDataConsumer
    {
        private const string EXTENSION = "fine-tunes";
        private const string PATH_DONE = "Done";

        private const string OPENAI_FINE_TUNES_RESPONSE = "OpenAI List Fine Tunes";

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
            ListFineTuneResponse fineTuneList = ListFineTuneResponse.JsonDeserialize(OpenAiRest.OpenAiGet(EXTENSION, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_FINE_TUNES_RESPONSE, fineTuneList);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(ListFineTuneResponse), OPENAI_FINE_TUNES_RESPONSE))
                };
            }
        }

        public DataDescription[] InputData { get; }
    }
}