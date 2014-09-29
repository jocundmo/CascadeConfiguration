using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadeConfiguration;
using System.IO;
using System.Xml;

namespace CascadeConfigurationTest
{
    public class TestConfigManager : ConfigManager
    {
        private static TestConfigManager _instance;
        private static string _location = "";

        static TestConfigManager()
        {
            _instance = new TestConfigManager();
            _instance.ReadRootConfig();
            _location = _instance._rootConfigNode.GetMetadata("EngineConfigurationFilePath");
            _instance.ReadConfig(_location);
            _instance.ReadApplicationConfig();
        }

        public static ConfigManager Instance
        {
            get
            {
                return _instance;
            }
        }
        protected override void ReadConfig(string basePath)
        {
            if (string.IsNullOrEmpty(basePath))
                basePath = Directory.GetCurrentDirectory();

            string[] fileNames = Directory.GetFiles(basePath, "*storyboard_config.xml");

            foreach (string fileName in fileNames)
            {
                XmlDocument xml = new XmlDocument();
                string fullFileName = fileName;
                xml.Load(fullFileName);
                XmlNodeList xnlRoot = xml.SelectNodes("//Document");
                if (xnlRoot.Count != 1)
                    throw new Exception("There should be one (and only one) <document> section defined");
                // Read Storyboard Config
                XmlNodeList xnlStoryboardConfigs = ((XmlElement)xnlRoot[0]).SelectNodes("StoryboardConfigs/StoryboardConfig");

                foreach (XmlElement xeStoryboardConfig in xnlStoryboardConfigs)
                {
                    string storyboardName = GetAttributeValue(xeStoryboardConfig, "name");
                    bool storyboardEnabled = ConvertToBoolean(GetAttributeValue(xeStoryboardConfig, "enabled", "true"));
                    StoryboradConfigNode storyboardConfigNode = new StoryboradConfigNode(storyboardName, string.Empty, storyboardEnabled, _rootConfigNode);
                    SetCommonProperties(storyboardConfigNode, xeStoryboardConfig);

                    try
                    {
                        this._configNodeDict.Add(storyboardConfigNode.Name.ToLowerInvariant(), storyboardConfigNode);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ArgumentException(string.Format("Error found when adding storyboard config node {0} to the engine... please check whether there is duplicated storyboard name...", storyboardConfigNode.Name), ex);
                    }
                    // Read Action Config
                    XmlNodeList xnlActionConfigs = xeStoryboardConfig.SelectNodes("ActionConfigs/ActionConfig");
                    foreach (XmlElement xeActionConfig in xnlActionConfigs)
                    {
                        string actionName = GetAttributeValue(xeActionConfig, "name");
                        bool actionEnabled = ConvertToBoolean(GetAttributeValue(xeActionConfig, "enabled", "true"));
                        ActionConfigNode actionConfigNode = new ActionConfigNode(actionName, string.Empty, actionEnabled, storyboardConfigNode);
                        SetCommonProperties(actionConfigNode, xeActionConfig);
                        storyboardConfigNode.ChildNodeDict.Add(actionConfigNode.Name.ToLowerInvariant(), actionConfigNode);

                        // Read DataAccess Config
                        XmlNodeList xnlDataAccessConfigs = xeActionConfig.SelectNodes("DataAccessConfig");
                        foreach (XmlElement xeDataAccessConfig in xnlDataAccessConfigs)
                        {
                            string daName = GetAttributeValue(xeDataAccessConfig, "name");
                            bool daEnabled = ConvertToBoolean(GetAttributeValue(xeDataAccessConfig, "enabled", "true"));
                            //string daInterface = GetAttributeValue(xeDataAccessConfig, "interface");
                            //string daClass = GetAttributeValue(xeDataAccessConfig, "class");

                            DatabaseAccessConfigNode daConfigNode = new DatabaseAccessConfigNode(daName, string.Empty, daEnabled, actionConfigNode);
                            SetCommonProperties(daConfigNode, xeDataAccessConfig);
                            actionConfigNode.ChildNodeDict.Add(daConfigNode.Name.ToLowerInvariant(), daConfigNode);
                        }
                    }
                }
            }
        }

        public override ConfigNode GetConfig(string cascadeName)
        {
            if (cascadeName == "root")
                return _rootConfigNode;

            string[] nameArray = cascadeName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            string sbName = nameArray.Length > 0 ? nameArray[0] : string.Empty;
            string actName = nameArray.Length > 1 ? nameArray[1] : string.Empty;
            string daName = nameArray.Length > 2 ? nameArray[2] : string.Empty;

            return GetConfig(sbName, actName, daName);
        }

        public ConfigNode GetConfig(string storyboardName, string actionName, string dataAccessName)
        {
            storyboardName = storyboardName.ToLowerInvariant();
            actionName = actionName.ToLowerInvariant();
            dataAccessName = dataAccessName.ToLowerInvariant();

            if (!string.IsNullOrEmpty(dataAccessName))
            {
                if (_configNodeDict[storyboardName].ChildNodeDict[actionName].ChildNodeDict.ContainsKey(dataAccessName))
                    return _configNodeDict[storyboardName].ChildNodeDict[actionName].ChildNodeDict[dataAccessName];
                else
                    return null;
            }
            else if (!string.IsNullOrEmpty(actionName))
            {
                if (_configNodeDict[storyboardName].ChildNodeDict.ContainsKey(actionName))
                    return _configNodeDict[storyboardName].ChildNodeDict[actionName];
                else
                    return null;
            }
            else if (!string.IsNullOrEmpty(storyboardName))
            {
                if (_configNodeDict.ContainsKey(storyboardName))
                    return _configNodeDict[storyboardName];
                else
                    return null;
            }
            else
                return null;
        }
    }
}
