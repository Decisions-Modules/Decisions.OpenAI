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
    public class OpenAISettings : AbstractModuleSettings, IInitializable
    {
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

        public void Initialize()
        {
            ModuleSettingsAccessor<OpenAISettings>.GetSettings();
            
            string? basePath = Path.GetDirectoryName(DecisionsFramework.ServiceLayer.Settings.SettingsFilePath);
            LogConstants.SYSTEM.Error($"Base Path: {basePath}");
            if (basePath != null)
            {
                string iconFilePathToWrite = Path.Combine(basePath, "images", "flow step images", "openai.svg");
                LogConstants.SYSTEM.Error($"Icon Path: {iconFilePathToWrite}");

                if (!File.Exists(iconFilePathToWrite))
                {
                    byte[] icon = LoadBytesFromResource("Decisions.OpenAI.openai.svg");
                    File.WriteAllBytes(iconFilePathToWrite, icon);
                }
            }
        }
        
        private static byte[] LoadBytesFromResource(string resourceName)
        {
            var assembly = typeof(OpenAISettings).Assembly;
            byte[] buffer = new byte[16 * 1024];

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    
                    while (stream != null && (read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    
                    return ms.ToArray();
                }
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