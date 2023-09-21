using System;
using Sirenix.OdinInspector;

namespace Luban.Editor
{
    [Serializable]
    public class CustomXargs
    {
        [VerticalGroup("键")]
        [HideLabel]
        [ValueDropdown("@LubanExportConfig.Instance.dropdown.GetKeyDropdown()")]
        public string key;

        [VerticalGroup("值")]
        [ValueDropdown("@LubanExportConfig.Instance.dropdown.GetDropdown(key, false)")]
        [HideLabel]
        public string value;
    }
}