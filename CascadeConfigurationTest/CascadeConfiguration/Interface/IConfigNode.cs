using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadeConfiguration
{
    public interface IConfigNode
    {
        string GetMetadata(string key);
        string GetMetadata(string key, string defaultValue);
        void SetMetadata(string key, string value);
        bool ContainsKey(string key);
    }
}
