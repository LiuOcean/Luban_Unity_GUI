using System;
using Sirenix.OdinInspector;

namespace Luban.Editor
{
    [Serializable]
    public class CodeTarget
    {
        [VerticalGroup("代码类型")]
        [HideLabel]
        [ValueDropdown("@LubanExportConfig.Instance.dropdown.GetDropdown(\"code_target\", false)")]
        public string code_type;

        [FolderPath]
        [VerticalGroup("输出文件夹")]
        [HideLabel]
        public string output_dir;
    }
}