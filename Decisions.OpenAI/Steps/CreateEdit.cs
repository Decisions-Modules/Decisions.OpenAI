using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiEdit;
using Decisions.OpenAI.Settings;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Edit", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, OpenAISettings.OPEN_AI_IMAGES_PATH)]
    public class CreateEdit : ISyncStep, IDataConsumer
    {
        private const string OPENAI_EDIT_RESPONSE = "OpenAiEdit";
        
        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }

        [WritableValue]
        private string editsModel = "text-davinci-edit-001";
        
        [PropertyClassification(1, "Chat Model", "Settings")]
        [SelectStringEditor(nameof(EditsModels))]
        public string EditsModel { get { return editsModel; } set { editsModel = value; } }
        
        [PropertyHidden(true)]
        public string[] EditsModels
        {
            get
            {
                return new[]
                {
                    "text-davinci-edit-001",
                    "code-davinci-edit-001"
                };
            }
        }
        
        public ResultData Run(StepStartData data)
        {
            string extension = "edits";
            string? input = data["input"] as string;
            string? instruction = data["instruction"] as string;
            EditRequest request = new EditRequest();

            request.Model = EditsModel;
            request.Input = input;
            request.Instruction = instruction;

            string editRequest = request.JsonSerialize();
            EditResponse editResponse = EditResponse.JsonDeserialize(OpenAiRest.OpenAiPost(editRequest, extension, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_EDIT_RESPONSE, editResponse);

            return new ResultData("Done", resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData("Done", new DataDescription(typeof(EditResponse), OPENAI_EDIT_RESPONSE))
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
                    new DataDescription(typeof(string), "input"),
                    new DataDescription(typeof(string), "instruction")
                });
            
                return input.ToArray();
            }
        }
    }
}