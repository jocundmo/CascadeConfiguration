using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public interface IDatabaseAccessConfig : IConfigNode
    {
        bool RequiredPersistData { get; set; }
        string TestCaseName { get; set; }
        string TestDataPath { get; set; }
        string ConnectionStringName { get; set; }
        string ClassName { get; set; }
        string MockClassName { get; set; }
        string InterfaceName { get; set; }
        string ClassNameInUse { get; set; }
        string Namespace { get; set; }
    }
}
