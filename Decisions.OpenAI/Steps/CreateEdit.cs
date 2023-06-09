using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiEdit;
using Decisions.OpenAI.Settings;
using DecisionsFramework;
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
        private const string EXTENSION = "edits";
        private const string PATH_DONE = "Done";
        
        private const string INPUT = "Input";
        private const string INSTRUCTION = "Instruction";
        private const string OPENAI_EDIT_RESPONSE = "OpenAI Edit";
        
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
            string? input = data[INPUT] as string;
            string? instruction = data[INSTRUCTION] as string;
            
            if (string.IsNullOrEmpty(instruction))
            {
                throw new BusinessRuleException($"{INSTRUCTION} cannot be null or empty.");
            }

            EditRequest request = new EditRequest();

            request.Model = EditsModel;
            request.Input = input;
            request.Instruction = instruction;

            string editRequest = request.JsonSerialize();
            EditResponse editResponse = EditResponse.JsonDeserialize(OpenAiRest.OpenAiPost(editRequest, EXTENSION, ApiKeyOverride));

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_EDIT_RESPONSE, editResponse);

            return new ResultData("Done", resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(EditResponse), OPENAI_EDIT_RESPONSE)
                    {
                        DisplayName = OPENAI_EDIT_RESPONSE
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
                    new DataDescription(typeof(string), INPUT),
                    new DataDescription(typeof(string), INSTRUCTION)
                });
            
                return input.ToArray();
            }
        }
    }
}