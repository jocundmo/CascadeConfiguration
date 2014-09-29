using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigNode cn = TestConfigManager.Instance.GetConfig("LNVStoryboard.FetchCommonPropertiesAction.IFetchSPTimetoutValueDataAccess");
        }
    }
}
