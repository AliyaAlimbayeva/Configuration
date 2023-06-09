using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration; 

namespace Configuration
{
    public class MyConfiguration : ConfigurationComponentBase
    {
        [ConfigurationItem("UserName", typeof(FileConfigurationProvider))]
        public string UserName { get; set; }

        [ConfigurationItem("Timeout", typeof(ConfigurationManagerConfigurationProvider))]
        public TimeSpan Timeout { get; set; }

        [ConfigurationItem("RetryCount", typeof(FileConfigurationProvider))]
        public int RetryCount { get; set; }
    }
}
