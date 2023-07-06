using System;
using System.Collections.Generic;
using DecisionsFramework;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.CoreSteps;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.ServiceLayer.Services.ExternalServiceReference.RESTService;
using DecisionsFramework.ServiceLayer.Services.ServiceInformation;
using DecisionsFramework.ServiceLayer.Utilities;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [ValidationRules]
    [ShapeImageAndColorProvider(null, "flow step images|type_json.svg")]
    [AutoRegisterStep("Get Json Schema", "Integration/OpenAI/Schema")]
    public class GetJsonSchemaOpenAi : BaseFlowAwareStep, ISyncStep, IDataConsumer
    {
        private const string OUTPUT_PATH_NAME = "Done";
        private const string NO_MATCHES_FOUND = "No Matches Found";
        private const string TYPE_Name = "Type Name";

        public override OutcomeScenarioData[] OutcomeScenarios
        {
            get
            {
                return new[]
              {
                    new OutcomeScenarioData(OUTPUT_PATH_NAME, new DataDescription(new DecisionsNativeType(typeof (string)), "JSON Schema", false, true, false)),
                    new OutcomeScenarioData(NO_MATCHES_FOUND),
              };
            }
        }

        public DataDescription[] InputData
        {
            get
            {
                return new[]
                    {
                        new DataDescription(new DecisionsNativeType(typeof (DecisionsType)), TYPE_Name, false, false, false)
                    };
            }
        }

        public ResultData Run(StepStartData data)
        {
            DecisionsType structure = data.Data[TYPE_Name] as DecisionsType;
            if (structure != null)
            {
                TypeIntegrationStructure jsonSchema = ServiceInformationService.Instance.GetTypeIntegrationData(UserContextHolder.GetCurrent(), structure.GetTypeName(), RestContentType.Json);

                if (jsonSchema != null && string.IsNullOrEmpty(jsonSchema.SchemaStructureString) == false)
                {
                    Dictionary<String, Object> resultData = new Dictionary<String, Object>();
                    resultData.Add("JSON Schema", jsonSchema.SchemaStructureString);
                    return new ResultData(OUTPUT_PATH_NAME, resultData);
                }
            }

            return new ResultData(NO_MATCHES_FOUND);
        }
    }
}
