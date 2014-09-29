using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public class AppliationConfigNode : ConfigNode, IApplicationConfig
    {
        public bool EnabledLog
        {
            get { return bool.Parse(GetMetadata("EnabledLog", "true")); }
            set { SetMetadata("EnabledLog", value.ToString()); }
        }
        public int CurrentLogLevel
        {
            get { return int.Parse(GetMetadata("CurrentLogLevel", "0")); }
            set { SetMetadata("CurrentLogLevel", value.ToString()); }
        }
        public string LogBasePath
        {
            get { return GetMetadata("LogBasePath"); }
            set { SetMetadata("LogBasePath", value); }
        }
        public bool PerformanceOptimize
        {
            get { return bool.Parse(GetMetadata("PerformanceOptimize", "true")); }
            set { SetMetadata("PerformanceOptimize", value.ToString()); }
        }
    }
}
