using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public interface IStoryboardConfig : IConfigNode
    {
        string TestCaseName { get; set; }
        string TestDataPath { get; set; }
    }
}
