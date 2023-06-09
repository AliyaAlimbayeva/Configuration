using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Configuration
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public object GetValue(string settingName, Type valueType)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string valueString = appSettings[settingName];

            if (valueString != null)
            {
                object value = ConvertValue(valueString, valueType);
                return value;
            }
            return null;
        }

        public void SetValue(string settingName, object value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[settingName] == null)
                {
                    settings.Add(settingName, value.ToString());
                }
                else
                {
                    settings[settingName].Value = value.ToString();
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private object ConvertValue(string valueString, Type valueType)
        {
            if (valueType == typeof(string))
            {
                return valueString;
            }
            else if (valueType == typeof(int))
            {
                return int.Parse(valueString);
            }
            else if (valueType == typeof(float))
            {
                return float.Parse(valueString);
            }
            else if (valueType == typeof(TimeSpan))
            {
                return TimeSpan.Parse(valueString);
            }
            else
            {
                throw new NotSupportedException($"Conversion to {valueType} is not supported.");
            }
        }
    }


}
