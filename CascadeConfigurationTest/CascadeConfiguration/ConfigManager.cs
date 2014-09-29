using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml;

namespace CascadeConfiguration
{
    public abstract class ConfigManager
    {
        protected Dictionary<string, ConfigNode> _configNodeDict = new Dictionary<string, ConfigNode>();
        protected ConfigNode _rootConfigNode = new ConfigNode("root", string.Empty, true, null);

        protected void ReadRootConfig()
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                _rootConfigNode.SetMetadata(key, ConfigurationManager.AppSettings[key].Trim());
            }
        }
        /// <summary>
        /// Get config value.
        /// </summary>
        /// <param name="cascadeName">cascade name split by {StoryboardName}.{ActionName}.{DataAccessName}</param>
        /// <returns></returns>
        public virtual ConfigNode GetConfig(string cascadeName)
        {
            throw new NotImplementedException();
        }
        
        protected void ReadApplicationConfig()
        {
            foreach (string key in _configNodeDict.Keys)
            {
                _rootConfigNode.ChildNodeDict[key] = _configNodeDict[key];
            }
        }
        protected virtual void ReadConfig(string basePath)
        {
            throw new NotImplementedException();
        }

        protected static void SetCommonProperties(ConfigNode configNode, XmlElement xmlNode)
        {
            foreach (XmlAttribute attr in xmlNode.Attributes)
            {
                configNode.SetMetadata(attr.Name, attr.Value);
            }
        }

        public static bool ConvertToBoolean(string b)
        {
            if (0 == string.Compare(b, bool.FalseString, true))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int ConvertToInt(string b)
        {
            return int.Parse(b);
        }

        public static string GetAttributeValue(XmlNode node, string key)
        {
            return GetAttributeValue(node, key, string.Empty);
        }
        public static string GetAttributeValue(XmlNode node, string key, string defaultValue)
        {
            XmlAttribute attribute = null;
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    attribute = attr;
                    break;
                }
            }
            string result = defaultValue;

            if (attribute != null)
                result = attribute.Value.Trim();

            return result;
        }
    }
}
