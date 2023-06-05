using System.Collections.Generic;
using Decisions.OpenAI.DataTypes.OpenAiChat;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Flow.StepImplementations;
using DecisionsFramework.Design.Properties;
using Newtonsoft.Json;

namespace Decisions.OpenAI.Steps
{
    [Writable]
    [AutoRegisterStep("Create Chat Completion", "Integration/OpenAI")]
    [ShapeImageAndColorProvider(null, "flow step images|openai.svg")]
    public class CreateChatCompletion : ISyncStep, IDataConsumer
    {
        private const string CONVERSATION = "Conversation";
        private const string PATH_DONE = "Done";

        private const string MESSAGE = "Message";
        private const string CLEAR_CONVERSATION = "Clear Conversation";
        private const string OPENAI_CHAT_COMPLETION_RESPONSE = "OpenAiChatCompletion";
        
        [WritableValue]
        private string apiKeyOverride;

        [PropertyClassification(0, "API Key Override", "Settings")]
        public string ApiKeyOverride
        {
            get => apiKeyOverride;
            set => apiKeyOverride = value;
        }

        [WritableValue]
        private string chatModel = "gpt-3.5-turbo";

        [PropertyClassification(1, "Chat Model", "Settings")]
        [SelectStringEditor(nameof(ChatModels))]
        public string ChatModel
        {
            get => chatModel;
            set => chatModel = value;
        }
        
        [PropertyHidden(true)]
        public string[] ChatModels
        {
            get
            {
                return new[]
                {
                    "gpt-4",
                    "gpt-4-32k",
                    "gpt-3.5-turbo"
                };
            }
        }

        private const string CHAT_COMPLETION_ID = "flow_data_openai_chat_completion_id";

        public ResultData Run(StepStartData data)
        {
            string extension = "chat/completions";
            string? message = data.Data[MESSAGE] as string;
            bool clearConversation = data.Data[CLEAR_CONVERSATION] is bool ? (bool)data.Data[CLEAR_CONVERSATION] : false;
            ChatRequest request;
            
            AbstractFlowTrackingData trackingData = FlowEngine.GetFlowTrackingData(data.FlowTrackingID);
            RunningStepData sData = trackingData.GetFlowStepRunData(data.StepTrackingID);
            
            if (clearConversation || !sData.FlowDataAtStepStart.ContainsKey(CHAT_COMPLETION_ID))
            {
                request = new ChatRequest();
                request.Messages = new List<ChatMessage>();
            }
            else
            {
                request = JsonConvert.DeserializeObject<ChatRequest>((string)sData.FlowDataAtStepStart[CHAT_COMPLETION_ID]);
            }
            
            request.Messages.Add(new ChatMessage(){role = "user", content = message});
            
            request.Model = ChatModel;

            string messageRequest = request.JsonSerialize();
            
            ChatResponse chatResponse = ChatResponse.JsonDeserialize(OpenAiRest.OpenAiPost(messageRequest, extension, ApiKeyOverride));
            
            request.Messages.Add(chatResponse.choices[chatResponse.choices.Count-1].message);

            string conversation = null;
            
            foreach (ChatMessage chat in request.Messages)
            {
                conversation += $"{chat.role}: {chat.content}\n";
            }

            data[CHAT_COMPLETION_ID] = request.JsonSerialize();
            
            object iterator = trackingData.GetTopTrackingData().GetExecutionData(CHAT_COMPLETION_ID);

            if (iterator == null)
            {
                sData.FlowDataAtStepStart[CHAT_COMPLETION_ID] = data[CHAT_COMPLETION_ID];
            }

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add(OPENAI_CHAT_COMPLETION_RESPONSE, chatResponse);
            resultData.Add(CONVERSATION, conversation);

            return new ResultData(PATH_DONE, resultData);
        }

        public OutcomeScenarioData[] OutcomeScenarios {
            get
            {
                return new[]
                {
                    new OutcomeScenarioData(PATH_DONE, new []
                    {
                        new DataDescription(typeof(ChatResponse), OPENAI_CHAT_COMPLETION_RESPONSE),
                        new DataDescription(typeof(string), CONVERSATION)
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
                    new DataDescription(typeof(string), MESSAGE),
                    new DataDescription(typeof(bool), CLEAR_CONVERSATION)
                });
            
                return input.ToArray();
            }
        }
    }
}