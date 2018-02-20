using System.Collections.Generic;
using NuGet.Server.Core.Infrastructure;

namespace MultiFeedNugetServerOwin
{
    public class LightNuGetFeedSettings : ISettingsProvider
    {
        public string Name { get; set; } = "Default";
        public string ApiKey { get; set; }
        public bool RequiresApiKey { get; set; } = true;
        public Dictionary<string, bool> NuGetServerSettings { get; set; } = new Dictionary<string, bool>();

        public bool GetBoolSetting(string key, bool defaultValue)
        {
            bool tmp;
            return NuGetServerSettings.TryGetValue(key, out tmp) ? tmp : defaultValue;
        }

        public string GetStringSetting(string key, string defaultValue)
        {
            return GetBoolSetting(key, false).ToString();
        }
    }
}