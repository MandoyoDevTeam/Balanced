using System;
using System.Configuration;

namespace Balanced.Config
{
    internal class ApiKeySettingsElement : ConfigurationElement
    {

        [ConfigurationProperty("secret", IsRequired = true)]
        public String Secret
        {
            get
            {
                return (String)this["secret"];
            }
            protected set
            {
                this["secret"] = value;
            }
        }
    }
}
