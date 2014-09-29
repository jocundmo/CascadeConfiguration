using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;

namespace CascadeConfigurationTest
{
    public class DatabaseAccessConfigNode : ConfigNode, IDatabaseAccessConfig
    {
        public string Namespace
        {
            get { return GetMetadata("Namespace") + ".DatabaseAccess"; }
            set { SetMetadata("Namespace", value); }
        }
        public string InterfaceName
        {
            get { return GetMetadata("InterfaceName"); }
            set { SetMetadata("InterfaceName", value); }
        }
        public string ClassName
        {
            get { return GetMetadata("ClassName"); }
            set { SetMetadata("ClassName", value); }
        }
        public string ClassNameInUse
        {
            get { return GetMetadata("ClassNameInUse", "ClassName"); }
            set { SetMetadata("ClassNameInUse", value); }
        }
        public string MockClassName
        {
            get { return GetMetadata("MockClassName"); }
            set { SetMetadata("MockClassName", value); }
        }
        public bool RequiredPersistData
        {
            get { return bool.Parse(GetMetadata("RequiredPersistData", "false")); }
            set { SetMetadata("RequiredPersistData", value.ToString()); }
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
        public string ConnectionStringName
        {
            get { return GetMetadata("ConnectionStringName", "RSRegionalDB"); }
            set { SetMetadata("ConnectionStringName", value); }
        }

        public DatabaseAccessConfigNode(string name, string value, bool enabled, ConfigNode parent)
            : base(name, value, enabled, parent)
        {
        }
    }
}
