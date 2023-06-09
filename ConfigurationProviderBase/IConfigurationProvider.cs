namespace Configuration
{
    public interface IConfigurationProvider
    {
        object GetValue(string settingName, Type valueType);
        void SetValue(string settingName, object value);
    }
}
