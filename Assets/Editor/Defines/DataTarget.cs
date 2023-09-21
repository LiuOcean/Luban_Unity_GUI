using System;
using Sirenix.OdinInspector;

namespace Luban.Editor
{
    [Serializable]
    public class DataTarget
    {
        [VerticalGroup("数据类型")]
        [HideLabel]
        [ValueDropdown("@LubanExportConfig.Instance.dropdown.GetDropdown(\"data_target\", false)")]
        public string data_type;

        [FolderPath]
        [VerticalGroup("输出文件夹")]
        [HideLabel]
        public string output_dir;
    }
}