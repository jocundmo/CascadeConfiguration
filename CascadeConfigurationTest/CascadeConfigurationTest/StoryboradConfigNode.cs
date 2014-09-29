using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public class StoryboradConfigNode : ConfigNode, IStoryboardConfig
    {
        public string Namespace
        {
            get { return GetMetadata("Namespace"); }
            set { SetMetadata("Namespace", value); }
        }

        public string Operand
        {
            get { return GetMetadata("Operand"); }
            set { SetMetadata("Operand", value); }
        }

        public string TestCaseName
        {
            get { return GetMetadata("TestCaseName"); }
            set { SetMetadata("TestCaseName", value); }
        }
        public string TestDataPath
        {
            get { return GetMetadata("TestDataPath"); }
            set { SetMetadata("TestDataPath", value); }
        }

        public StoryboradConfigNode(string name, string value, bool enabled, ConfigNode parent)
            : base(name, value, enabled, parent)
        {
        }
    }
}
