using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Luban.Editor
{
    [Serializable]
    public class CustomDropDownDic : UnitySerializedDictionary<string, string>
    {
        public IEnumerable<ValueDropdownItem<string>> GetKeyDropdown()
        {
            foreach(var key in Keys)
            {
                yield return new ValueDropdownItem<string>(key, key);
            }
        }

        public IEnumerable<ValueDropdownItem<string>> GetDropdown(string key, bool append_empty)
        {
            if(key is null)
            {
                yield return new ValueDropdownItem<string>("", "");
                yield break;
            }

            TryGetValue(key, out var values);

            if(append_empty)
            {
                yield return new ValueDropdownItem<string>("无", "");
            }

            if(string.IsNullOrEmpty(values))
            {
                yield break;
            }

            values = values.Replace(" ", "");

            foreach(var value in values.Split(","))
            {
                yield return new ValueDropdownItem<string>(value, value);
            }
        }
    }
}