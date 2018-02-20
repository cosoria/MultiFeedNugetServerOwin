using System;
using System.IO;
using System.Web;
using EnsureThat;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MultiFeedNugetServerOwin
{
    public class LightNugetServerSettingsJsonFactory
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = false
                }
            }
        };

        public static LightNuGetServerSettings Create(string jsonFilePath)
        {
            Ensure.String.IsNotNullOrWhiteSpace(jsonFilePath, nameof(jsonFilePath));

            if (VirtualPathUtility.IsAppRelative(jsonFilePath))
            {
                jsonFilePath = HttpContext.Current.Server.MapPath(jsonFilePath);
            }

            if (!File.Exists(jsonFilePath))
            {
                throw new Exception($"Settings file can not be found at: '{jsonFilePath}'.");
            }
            
            var json = File.ReadAllText(jsonFilePath);

            return JsonConvert.DeserializeObject<LightNuGetServerSettings>(json, JsonSettings);
        }
    }
}