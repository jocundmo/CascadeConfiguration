using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public class ActionConfigNode : ConfigNode, IActionConfig
    {
        public ActionConfigNode(string name, string value, bool enabled, ConfigNode parent)
            : base(name, value, enabled, parent)
        {
        }
    }
}
