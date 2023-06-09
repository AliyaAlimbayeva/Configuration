using System;
using System.Linq;
using System.Reflection;
using Configuration;

public abstract class ConfigurationComponentBase
{
    public void LoadSettings()
    {
        PropertyInfo[] properties = GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            ConfigurationItemAttribute attribute = (ConfigurationItemAttribute)property.GetCustomAttributes(typeof(ConfigurationItemAttribute), true)
                .FirstOrDefault();

            if (attribute != null)
            {
                IConfigurationProvider provider = (IConfigurationProvider)Activator.CreateInstance(attribute.ProviderType);
                object value = provider.GetValue(attribute.SettingName, property.PropertyType);
                property.SetValue(this, value);
            }
        }
    }

    public void SaveSettings()
    {
        PropertyInfo[] properties = GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            ConfigurationItemAttribute attribute = (ConfigurationItemAttribute)property
                .GetCustomAttributes(typeof(ConfigurationItemAttribute), true)
                .FirstOrDefault();

            if (attribute != null)
            {
                IConfigurationProvider provider = (IConfigurationProvider)Activator.CreateInstance(attribute.ProviderType);
                object value = property.GetValue(this);
                provider.SetValue(attribute.SettingName, value);
            }
        }
    }
}
