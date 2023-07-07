using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Decisions.OpenAI.Settings;
using DecisionsFramework;
using DecisionsFramework.ServiceLayer;

namespace Decisions.OpenAI
{
    public class OpenAiRest
    {
        public static string OPENAI_BASE_URL = "https://api.openai.com/v1";
        
        public static string? OpenAiGet(string extension, string apiKeyOverride)
        {
            string url = $"{OPENAI_BASE_URL}/{extension}";
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

            try
            {
                byte[] resp = client.DownloadData(url);
                return Task.FromResult(Encoding.UTF8.GetString(resp)).Result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(404)"))
                {
                    throw new Exception("(404) Not Found. Input parameters may not be correct. Verify model/file exists and OpenAI key has access to it.", ex);
                }
                
                if (ex.Message.Contains("timed out"))
                {
                    throw new Exception("OpenAI took too long to respond and has timed out.", ex);
                }

                throw;
            }
        }

        public static string? OpenAiPost(string data, string extension, string apiKeyOverride)
        {
            string url = $"{OPENAI_BASE_URL}/{extension}";
            Byte[] postbytes = Encoding.UTF8.GetBytes(data);
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
            
            client.Headers.Add("Content-Type", "application/json");

            try
            {
                byte[] resp = client.UploadData(url, "POST", postbytes);
                return Task.FromResult(Encoding.UTF8.GetString(resp)).Result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(400)"))
                {
                    throw new Exception("(400) Bad Request. Verify parameter values are within an acceptable range.", ex);
                }
                
                if (ex.Message.Contains("(404)"))
                {
                    throw new Exception("(404) Not Found. Input parameters may not be correct. Verify model/file exists and OpenAI key has access to it.", ex);
                }
                
                if (ex.Message.Contains("(429)"))
                {
                    throw new Exception("(429) Too Many Requests. Verify credit availability at https://platform.openai.com/account/usage.", ex);
                }

                if (ex.Message.Contains("timed out"))
                {
                    throw new Exception("OpenAI took too long to respond and has timed out.", ex);
                }

                throw;
            }
        }

        public static string? OpenAiDelete(string extension, string apiKeyOverride)
        {
            string url = $"{OPENAI_BASE_URL}/{extension}";
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

            try
            {
                byte[] resp = client.UploadData(url, "DELETE", new byte[0]);
                return Task.FromResult(Encoding.UTF8.GetString(resp)).Result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(404)"))
                {
                    throw new Exception("(404) Not Found. Parameters may not be correct. Verify model/file exists and OpenAI key has access to it.", ex);
                }
                
                if (ex.Message.Contains("timed out"))
                {
                    throw new Exception("OpenAI took too long to respond and has timed out.", ex);
                }

                throw;
            }
        }

        private static string GetAPIKey()
        {
            OpenAISettings settings = ModuleSettingsAccessor<OpenAISettings>.GetSettings();
            return settings.ApiKey;
        }
    }
}