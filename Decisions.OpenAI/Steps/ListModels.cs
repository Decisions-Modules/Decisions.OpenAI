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
    [AutoRegisterStep("List Models", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(DecisionsFramework.ServiceLayer.Services.Image.ImageInfoType.Url, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class ListModels : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string OPENAI_LIST_MODELS_RESPONSE = "OpenAiListModels";
        
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
            string extension = "models";

            ModelList modelListResponse = ModelList.JsonDeserialize(OpenAiRest.OpenAiGet(extension, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_LIST_MODELS_RESPONSE, modelListResponse);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(ModelList), OPENAI_LIST_MODELS_RESPONSE))
                };
            }
        }

        public DataDescription[] InputData { get; }
    }
}