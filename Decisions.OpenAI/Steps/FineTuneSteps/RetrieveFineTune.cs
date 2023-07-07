using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiFineTune;
using Decisions.OpenAI.Settings;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps.FineTuneSteps
{
    [Writable]
    [AutoRegisterStep("Retrieve Fine Tune", "Integration/OpenAI/Fine-Tune")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class RetrieveFineTune : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";

        private const string FINE_TUNE_ID = "Fine Tune ID";
        private const string OPENAI_FINE_TUNE_RESPONSE = "OpenAiRetrieveFineTune";

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
            string fineTuneId = data[FINE_TUNE_ID] as string;

            if (string.IsNullOrEmpty(fineTuneId))
            {
                throw new BusinessRuleException($"{FINE_TUNE_ID} cannot be null or empty.");
            }

            string extension = $"fine-tunes/{fineTuneId}";

            FineTuneResponse fineTune = FineTuneResponse.JsonDeserialize(OpenAiRest.OpenAiGet(extension, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_FINE_TUNE_RESPONSE, fineTune);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(FineTuneResponse), OPENAI_FINE_TUNE_RESPONSE))
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
                    new DataDescription(typeof(string), FINE_TUNE_ID)
                });

                return input.ToArray();
            }
        }
    }
}