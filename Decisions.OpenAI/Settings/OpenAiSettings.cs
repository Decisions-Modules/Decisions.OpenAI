using System.Collections.Generic;
using System.IO;
using DecisionsFramework.Data.ORMapper;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.Design.Properties.Attributes;
using DecisionsFramework.ServiceLayer;
using DecisionsFramework.ServiceLayer.Actions;
using DecisionsFramework.ServiceLayer.Actions.Common;
using DecisionsFramework.ServiceLayer.Utilities;

namespace Decisions.OpenAI.Settings
{
    [Writable]
    public class OpenAISettings : AbstractModuleSettings
    {
        internal const string OPEN_AI_IMAGES_PATH = "../wwwroot/Content/CustomModuleImages/Decisions.OpenAI/|openai.svg";

        public OpenAISettings()
        {
            this.EntityName = "OpenAI Settings";
        }
        
        [ORMField]
        private string apiKey;
        
        [PropertyClassification(0, " ", "OpenAI Settings")]
        [ReadonlyEditor]
        [ExcludeInDescription]
        public string ApiKeyMessage
        {
            get
            {
                return "An API Key must be retrieved from https://platform.openai.com/account/api-keys";
            }
            set { }
        }

        [WritableValue]
        [PropertyClassification(1, "API Key", "OpenAI Settings")]
        public string ApiKey
        {
            get
            {
                return apiKey;
            }
            set
            {
                apiKey = value;
            }
        }       
   

        public override BaseActionType[] GetActions(AbstractUserContext userContext, EntityActionType[] types)
        {
            List<BaseActionType> actions = new List<BaseActionType>();
            actions.Add(new EditEntityAction(GetType(), "Edit Settings", "Edit OpenAI Settings", null));

            return actions.ToArray();
        }
    }
}