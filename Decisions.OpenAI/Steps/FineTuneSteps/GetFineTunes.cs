using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiFineTune;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps.FineTuneSteps
{
    [Writable]
    [AutoRegisterStep("Get Fine Tunes", "Integration/OpenAI/Fine-Tune")]
    [ShapeImageAndColorProvider(null, "flow step images|openai.svg")]
    public class GetFineTunes : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string FINE_TUNE_ID = "fineTuneId";
        private const string OPENAI_FINE_TUNES_RESPONSE = "OpenAiGetFineTunes";
        
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
            
            string extension = "fine-tunes";
            
            // returns all fine tunes if 'fineTune' is null
            if (!string.IsNullOrEmpty(fineTuneId))
                extension += '/' + fineTuneId;

            ListFineTuneResponse fineTuneList = ListFineTuneResponse.JsonDeserialize(OpenAiRest.OpenAiGet(extension, ApiKeyOverride));
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_FINE_TUNES_RESPONSE, fineTuneList);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(ListFineTuneResponse), OPENAI_FINE_TUNES_RESPONSE))
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