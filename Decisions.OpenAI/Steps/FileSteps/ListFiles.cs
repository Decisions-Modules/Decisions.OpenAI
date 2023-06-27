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
    [ShapeImageAndColorProvider(DecisionsFramework.ServiceLayer.Services.Image.ImageInfoType.Url, $"{OpenAISettings.OPEN_AI_IMAGES_PATH}")]
    public class ListFiles : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string OPENAI_LIST_FILES_RESPONSE = "OpenAiListFiles";
        
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
            string extension = "files";

            string? resp = OpenAiRest.OpenAiGet(extension, ApiKeyOverride);

            OpenAiFileContainer listFilesResponse = JsonConvert.DeserializeObject<OpenAiFileContainer>(resp);
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_LIST_FILES_RESPONSE, listFilesResponse);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(OpenAiFileContainer), OPENAI_LIST_FILES_RESPONSE))
                };
            }
        }

        public DataDescription[] InputData { get; }
    }
}