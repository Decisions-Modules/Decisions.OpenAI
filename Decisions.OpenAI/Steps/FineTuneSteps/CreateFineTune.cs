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
    [AutoRegisterStep("Create Fine Tune", "Integration/OpenAI/Fine-Tune")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class CreateFineTune : ISyncStep, IDataConsumer
    {
        private const string EXTENSION = "fine-tunes";
        private const string PATH_DONE = "Done";
        
        private const string TRAINING_FILE_ID = "Training File ID";
        private const string OPENAI_FINE_TUNE_RESPONSE = "OpenAI Create Fine Tune";

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
            string trainingFileId = data[TRAINING_FILE_ID] as string;
            
            if (string.IsNullOrEmpty(trainingFileId))
            {
                throw new BusinessRuleException($"{TRAINING_FILE_ID} cannot be null or empty.");
            }

            FineTuneRequest request = new FineTuneRequest();
            request.TrainingFile = trainingFileId;

            string fineTuneRequest = request.JsonSerialize();
            FineTuneResponse fineTuneResponse = FineTuneResponse.JsonDeserialize(OpenAiRest.OpenAiPost(fineTuneRequest, EXTENSION, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_FINE_TUNE_RESPONSE, fineTuneResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(FineTuneResponse), OPENAI_FINE_TUNE_RESPONSE)
                    {
                        DisplayName = OPENAI_FINE_TUNE_RESPONSE
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
                    new DataDescription(typeof(string), TRAINING_FILE_ID)
                });

                return input.ToArray();
            }
        }
    }
}