using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadeConfiguration
{
    public class ConfigNode : IConfigNode
    {
        public string Name
        {
            get { return GetMetadata("Name"); }
            set { SetMetadata("Name", value); }
        }
        public string Value { get; set; }
        public bool Enabled { get; set; }
        public ConfigNode Parent { get; set; }
        protected Dictionary<string, string> Metadata { get; set; }
        public Dictionary<string, ConfigNode> ChildNodeDict { get; protected set; }

        public void SetMetadata(string key, string val)
        {
            this.Metadata[key] = val;
        }

        public bool ContainsKey(string key)
        {
            string value = GetMetadata(key, null);
            return value != null;
        }

        public string GetMetadata(string key, string defaultValue)
        {
            if (Metadata.ContainsKey(key))
            {
                return Metadata[key];
            }
            else
            {
                if (Parent != null)
                {
                    return Parent.GetMetadata(key, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }
        public string GetMetadata(string key)
        {
            return GetMetadata(key, string.Empty);
        }
        public ConfigNode()
        {
            Metadata = new Dictionary<string, string>();
            ChildNodeDict = new Dictionary<string, ConfigNode>();
        }

        public ConfigNode(string name, string value, bool enabled, ConfigNode parent)
            : this()
        {
            this.Name = name;
            this.Value = value;
            this.Enabled = enabled;
            this.Parent = parent;
        }
    }
}
