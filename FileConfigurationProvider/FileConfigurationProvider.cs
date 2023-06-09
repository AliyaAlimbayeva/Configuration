using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class FileConfigurationProvider: IConfigurationProvider
    {
        private string filePath = "settings.txt";

        
        public object GetValue(string settingName, Type valueType)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');

                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string valueString = parts[1].Trim();

                        if (key == settingName)
                        {
                            object value = ConvertValue(valueString, valueType);
                            return value;
                        }
                    }
                }
            }
            return null;
        }

        public void SetValue(string settingName, object value)
        {
            string valueString = Convert.ToString(value);
            string[] lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];
            bool settingFound = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();

                    if (key == settingName)
                    {
                        lines[i] = $"{settingName}={valueString}";
                        settingFound = true;
                        break;
                    }
                }
            }
            if (!settingFound)
            {
                string newLine = $"{settingName}={valueString}";
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = newLine;
            }
            File.WriteAllLines(filePath, lines);
        }
        private object ConvertValue(string valueString, Type valueType)
        {
            // Convert the value string to the specified value type
            // You can use the built-in Convert.ChangeType method for basic types
            object value = Convert.ChangeType(valueString, valueType);
            return value;
        }
    }
}
