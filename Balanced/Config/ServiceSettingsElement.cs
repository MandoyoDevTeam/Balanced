using System.Configuration;

namespace Balanced.Config
{
    internal class ServiceSettingsElement : ConfigurationElement
    {

        [ConfigurationProperty("balancedUrl", DefaultValue ="https://api.balancedpayments.com", IsRequired = false)]
        public string BalancedUrl
        {
            get
            {
                return (string)this["balancedUrl"];
            }
            protected set
            {
                this["balancedUrl"] = value;
            }
        }

        [ConfigurationProperty("agent", DefaultValue = "balanced-csharp", IsRequired = false)]
        public string Agent
        {
            get
            {
                return (string)this["agent"];
            }
            protected set
            {
                this["agent"] = value;
            }
        }

        [ConfigurationProperty("version", DefaultValue = "1.1", IsRequired = false)]
        public string Version
        {
            get
            {
                return (string)this["version"];
            }
            protected set
            {
                this["version"] = value;
            }
        }

        [ConfigurationProperty("acceptType", DefaultValue = "application/vnd.api+json;revision=1.1", IsRequired = false)]
        public string AcceptType
        {
            get
            {
                return (string)this["acceptType"];
            }
            protected set
            {
                this["acceptType"] = value;
            }
        }

        [ConfigurationProperty("contentType", DefaultValue = "application/json", IsRequired = false)]
        public string ContentType
        {
            get
            {
                return (string)this["contentType"];
            }
            protected set
            {
                this["contentType"] = value;
            }
        }

        [ConfigurationProperty("encoding", DefaultValue = "utf-8", IsRequired = false)]
        public string Encoding
        {
            get
            {
                return (string)this["encoding"];
            }
            protected set
            {
                this["encoding"] = value;
            }
        }
    }
}
