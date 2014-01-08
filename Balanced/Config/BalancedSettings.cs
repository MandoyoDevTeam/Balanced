using System;
using System.Configuration;
using System.Text;

namespace Balanced.Config
{
    public static class BalancedSettings
    {
        [ThreadStatic] private static BalancedSection _balancedSection;

        private static readonly object Locker = new object();

        public static void Init()
        {
            lock (Locker)
            {
                _balancedSection = ConfigurationManager.GetSection("balanced") as BalancedSection;    
            }
            
        }

        public static void Init(string configurationPath, string sectionName = "balanced")
        {
            lock (Locker)
            {
                var map = new ExeConfigurationFileMap { ExeConfigFilename = configurationPath };
                _balancedSection = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None).GetSection(sectionName) as BalancedSection;    
            }
        }

        public static String Secret
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");

                return _balancedSection.ApiKeySettings.Secret;
            }
        }

        public static Uri BalancedUrl
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");

                return new Uri(_balancedSection.ServiceSettings.BalancedUrl);
            }
        }

        public static String Agent
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");
                return _balancedSection.ServiceSettings.Agent;
            }
        }

        public static String Version
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");
                return _balancedSection.ServiceSettings.Version;
            }
        }

        public static String AcceptType
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");
                return _balancedSection.ServiceSettings.AcceptType;
            }
        }

        public static String ContentType
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");
                return _balancedSection.ServiceSettings.ContentType;
            }
        }

        public static Encoding Encoding
        {
            get
            {
                if (_balancedSection == null) throw new InvalidOperationException("Configuration must be initialized first");
                return Encoding.GetEncoding(_balancedSection.ServiceSettings.Encoding);
            }
        }
    }
}
