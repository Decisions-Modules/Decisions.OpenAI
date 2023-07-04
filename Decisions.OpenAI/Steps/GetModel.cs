using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiModel;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Get Model", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class GetModel : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";

        private const string MODEL = "Model";
        private const string OPENAI_GET_MODEL_RESPONSE = "OpenAiGetModel";

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
            string model = data[MODEL] as string;

            string extension = $"models/{model}";

            OpenAiModel modelResponse = OpenAiModel.JsonDeserialize(OpenAiRest.OpenAiGet(extension, ApiKeyOverride));
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_GET_MODEL_RESPONSE, modelResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(OpenAiModel), OPENAI_GET_MODEL_RESPONSE))
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
                    new DataDescription(typeof(string), MODEL)
                });

                return input.ToArray();
            }
        }
    }
}