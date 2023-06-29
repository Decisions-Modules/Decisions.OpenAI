using System.Collections.Generic;
using Decisions.OpenAI.DataTypes;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps.FineTuneSteps
{
    [Writable]
    [AutoRegisterStep("Delete Fine Tune", "Integration/OpenAI/Fine-Tune")]
    [ShapeImageAndColorProvider(DecisionsFramework.ServiceLayer.Services.Image.ImageInfoType.Url, $"{OpenAISettings.OPEN_AI_IMAGES_PATH}")]
    public class DeleteFineTuneModel : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string FINE_TUNED_MODEL = "fineTunedModel";
        private const string OPENAI_DELETE_FINE_TUNE_RESPONSE = "OpenAiDeleteFineTune";
        
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
            string fineTunedModel = data[FINE_TUNED_MODEL] as string;
            
            string extension = $"models/{fineTunedModel}";

            DeleteResponse deleteFineTuneResponse = DeleteResponse.JsonDeserialize(OpenAiRest.OpenAiDelete(extension, ApiKeyOverride));
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_DELETE_FINE_TUNE_RESPONSE, deleteFineTuneResponse);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(DeleteResponse), OPENAI_DELETE_FINE_TUNE_RESPONSE))
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
                    new DataDescription(typeof(string), FINE_TUNED_MODEL)
                });
            
                return input.ToArray();
            }
        }
    }
}