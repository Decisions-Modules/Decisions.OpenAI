using System.Collections.Generic;
using Decisions.OpenAI.DataTypes;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.OpenAI.Steps.FileSteps
{
    [Writable]
    [AutoRegisterStep("Delete File", "Integration/OpenAI/Files")]
    [ShapeImageAndColorProvider(null, "flow step images|openai.svg")]
    public class DeleteFile : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string FILE_ID = "FileId";
        private const string OPENAI_DELETE_FILE_RESPONSE = "OpenAiDeleteFile";
        
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
            string fileId = data[FILE_ID] as string;
            
            string extension = $"files/{fileId}";

            string? resp = OpenAiRest.OpenAiDelete(extension, ApiKeyOverride);

            DeleteResponse deleteResponse = JsonConvert.DeserializeObject<DeleteResponse>(resp);
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_DELETE_FILE_RESPONSE, deleteResponse);
            
            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(DeleteResponse), OPENAI_DELETE_FILE_RESPONSE))
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
                    new DataDescription(typeof(string), FILE_ID)
                });
            
                return input.ToArray();
            }
        }
    }
}