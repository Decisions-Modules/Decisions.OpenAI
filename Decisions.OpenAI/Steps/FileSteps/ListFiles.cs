using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiFile;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.OpenAI.Steps.FileSteps
{
    [Writable]
    [AutoRegisterStep("List Files", "Integration/OpenAI/Files")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class ListFiles : ISyncStep, IDataConsumer
    {
        private const string EXTENSION = "files";
        private const string PATH_DONE = "Done";

        private const string OPENAI_LIST_FILES_RESPONSE = "OpenAI List Files";

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
            string? resp = OpenAiRest.OpenAiGet(EXTENSION, ApiKeyOverride);

            OpenAIFileContainer listFilesResponse = JsonConvert.DeserializeObject<OpenAIFileContainer>(resp);

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_LIST_FILES_RESPONSE, listFilesResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(OpenAIFileContainer), OPENAI_LIST_FILES_RESPONSE)
                    {
                        DisplayName = OPENAI_LIST_FILES_RESPONSE
                    })
                };
            }
        }

        public DataDescription[] InputData { get; }
    }
}