using Configuration;
class Program
{
    static void Main(string[] args)
    {
        MyConfiguration config = new MyConfiguration();
        config.LoadSettings();

        Console.WriteLine($"UserName: {config.UserName}");
        Console.WriteLine($"Timeout: {config.Timeout}");
        Console.WriteLine($"RetryCount: {config.RetryCount}");

        // Modify the settings
        config.UserName = "rrr";
        config.Timeout = TimeSpan.FromSeconds(56);
        config.RetryCount = 5;


        // Save the modified settings
        config.SaveSettings();
        Console.WriteLine($"UserName: {config.UserName}");
        Console.WriteLine($"Timeout: {config.Timeout}");
        Console.WriteLine($"RetryCount: {config.RetryCount}");
    }
}

