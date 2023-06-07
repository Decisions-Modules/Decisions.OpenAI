using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiEmbedding;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Embedding", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, "flow step images|openai.svg")]
    public class CreateEmbedding : ISyncStep, IDataConsumer
    {
        private const string OPENAI_EMBEDDING_RESPONSE = "OpenAiEmbedding";
        
        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }

        [WritableValue]
        private string embeddingsModel = "text-embedding-ada-002";
        
        [PropertyClassification(1, "Chat Model", "Settings")]
        [SelectStringEditor(nameof(EmbeddingsModels))]
        public string EmbeddingsModel { get { return embeddingsModel; } set { embeddingsModel = value; } }
        
        [PropertyHidden(true)]
        public string[] EmbeddingsModels
        {
            get
            {
                return new[]
                {
                    "text-embedding-ada-002",
                    "text-search-ada-doc-001"
                };
            }
        }

        public ResultData Run(StepStartData data)
        {
            string[] inputs = data["input"] as string[];
            
            string extension = "embeddings";
            EmbeddingRequest request = new EmbeddingRequest();

            request.Input = inputs;
            request.Model = EmbeddingsModel;
            string embeddingRequest = request.JsonSerialize();

            EmbeddingResponse embeddingResponse = EmbeddingResponse.JsonDeserialize(OpenAiRest.OpenAiPost(embeddingRequest, extension, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_EMBEDDING_RESPONSE, embeddingResponse);

            return new ResultData("Done", resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData("Done", new DataDescription(typeof(EmbeddingResponse), OPENAI_EMBEDDING_RESPONSE))
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
                    new DataDescription(typeof(string[]), "input")
                });
            
                return input.ToArray();
            }
        }
    }
}