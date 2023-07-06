using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Decisions.OpenAI.DataTypes.OpenAiFile;
using Decisions.OpenAI.Settings;
using DecisionsFramework;
using DecisionsFramework.Data.DataTypes;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.ServiceLayer;
using DecisionsFramework.ServiceLayer.Services.ExternalServiceReference.RESTService;
using Newtonsoft.Json;

namespace Decisions.OpenAI.Steps.FileSteps
{
    [Writable]
    [AutoRegisterStep("Upload File", "Integration/OpenAI/Files")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class UploadFile : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";

        private const string FILE = "File";
        private const string OPENAI_UPLOAD_FILE_RESPONSE = "OpenAiUploadFile";

        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }

        private static string GetAPIKey()
        {
            OpenAISettings settings = ModuleSettingsAccessor<OpenAISettings>.GetSettings();
            return settings.ApiKey;
        }

        public ResultData Run(StepStartData data)
        {
            FileData file = data[FILE] as FileData;
            
            if (file == null)
            {
                throw new BusinessRuleException($"{FILE} cannot be null or empty.");
            }
            
            string url = "https://api.openai.com/v1/files";
            string multiPartBoundary = $"Boundary-{Guid.NewGuid().ToString()}";

            WebClient client = new WebClient();

            if (!string.IsNullOrEmpty(apiKeyOverride))
            {
                client.Headers.Add("Authorization", "Bearer " + apiKeyOverride);
            }
            else if (!string.IsNullOrEmpty(GetAPIKey()))
            {
                client.Headers.Add("Authorization", "Bearer " + GetAPIKey());
            }
            else
            {
                throw new BusinessRuleException("An API key must be declared in the settings");
            }

            client.Headers.Add("Content-Type", $"multipart/form-data; boundary={multiPartBoundary}");

            MultiPartFormDataPart purposePart = new MultiPartFormDataPart
            {
                PartType = MultiPartFormDataPartType.Value,
                Name = "purpose",
                PartValue = "fine-tune"
            };

            MultiPartFormDataPart filePart = new MultiPartFormDataPart
            {
                PartType = MultiPartFormDataPartType.File,
                Name = "file",
                File = file
            };

            MultiPartFormDataPart[] formDataParts = { purposePart, filePart };

            byte[] requestBody = MultiPartFormDataPart.GetRequestBodyFromParts(formDataParts, multiPartBoundary);
            byte[] resp = client.UploadData(url, "POST", requestBody);

            Task<string> respTask = Task.FromResult(Encoding.UTF8.GetString(resp));

            OpenAiFile fileResponse = JsonConvert.DeserializeObject<OpenAiFile>(respTask.Result);

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_UPLOAD_FILE_RESPONSE, fileResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(OpenAiFile), OPENAI_UPLOAD_FILE_RESPONSE))
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
                    new DataDescription(typeof(FileData), FILE)
                });

                return input.ToArray();
            }
        }
    }
}