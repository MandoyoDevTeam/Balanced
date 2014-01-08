using System.Configuration;

namespace Balanced.Config
{
    internal class BalancedSection : ConfigurationSection
    {

        [ConfigurationProperty("apiKey", IsRequired = true)]
        public ApiKeySettingsElement ApiKeySettings
        {
            get
            {
                return (ApiKeySettingsElement)this["apiKey"];
            }
            protected set
            {
                this["apiKey"] = value;
            }
        }

        [ConfigurationProperty("serviceSettings", IsRequired = false)]
        public ServiceSettingsElement ServiceSettings
        {
            get
            {
                return (ServiceSettingsElement)this["serviceSettings"];
            }
            protected set
            {
                this["serviceSettings"] = value;
            }
        }

    }
}
