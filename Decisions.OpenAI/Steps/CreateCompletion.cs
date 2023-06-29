using System;
using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiCompletion;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Completion", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(DecisionsFramework.ServiceLayer.Services.Image.ImageInfoType.Url, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class CreateCompletion : ISyncStep, IDataConsumer
    {
        private const string PATH_DONE = "Done";
        
        private const string PROMPT = "Prompt";
        private const string MAX_TOKENS = "Max Tokens";
        private const string TEMPERATURE = "Temperature";
        private const string NUMBER_OF_COMPLETIONS = "Number Of Completions";
        private const string MODEL_NAME_OVERRIDE = "Model Name Override";
        private const string OPENAI_COMPLETION_RESPONSE = "OpenAiCompletion";
        
        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }
        
        [WritableValue]
        private string completionModel = "text-ada-001";

        [PropertyClassification(1, "Completion Model", "Settings")]
        [SelectStringEditor(nameof(CompletionModels))]
        public string CompletionModel
        {
            get => completionModel;
            set => completionModel = value;
        }
        
        [PropertyHidden(true)]
        public string[] CompletionModels
        {
            get
            {
                return new[]
                {
                    "text-davinci-003",
                    "text-davinci-002",
                    "text-curie-001",
                    "text-babbage-001",
                    "text-ada-001"
                };
            }
        }
        
        public ResultData Run(StepStartData data)
        {
            string extension = "completions";
            string prompt = data[PROMPT] as string;
            int maxTokens = data[MAX_TOKENS] is int ? (int)data[MAX_TOKENS] : 0;
            double temperature = data[TEMPERATURE] is double ? (double)data[TEMPERATURE] : 0;
            int n = data[NUMBER_OF_COMPLETIONS] is int ? (int)data[NUMBER_OF_COMPLETIONS] : 0;
            string? modelNameOverride = data[MODEL_NAME_OVERRIDE] as string;
            CompletionRequest request = new CompletionRequest();

            request.Model = CompletionModel;
            request.Prompt = prompt;
            request.MaxTokens = Math.Abs(maxTokens);
            request.Temperature = Math.Abs(temperature);
            request.N = Math.Abs(n);

            if (request.MaxTokens > 2048)
            {
                if ((request.Model == "text-davinci-003" || request.Model == "text-davinci-002")
                    && request.MaxTokens > 4096)
                {
                    request.MaxTokens = 4096;
                }
                else
                {
                    request.MaxTokens = 2048;
                }
            }

            if (request.Temperature > 1)
            {
                request.Temperature = 1;
            }

            if (request.N > 128)
            {
                request.N = 128;
            }
            
            if (!string.IsNullOrEmpty(modelNameOverride))
            {
                request.Model = modelNameOverride;
            }

            string messageRequest = request.JsonSerialize();
            CompletionResponse completionResponse = CompletionResponse.JsonDeserialize(OpenAiRest.OpenAiPost(messageRequest, extension, ApiKeyOverride));
            
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_COMPLETION_RESPONSE, completionResponse);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(CompletionResponse), OPENAI_COMPLETION_RESPONSE))
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
                    new DataDescription(typeof(string), PROMPT),
                    new DataDescription(typeof(int), MAX_TOKENS),
                    new DataDescription(typeof(double), TEMPERATURE),
                    new DataDescription(typeof(int), NUMBER_OF_COMPLETIONS),
                    new DataDescription(typeof(string), MODEL_NAME_OVERRIDE)
                });
            
                return input.ToArray();
            }
        }
    }
}