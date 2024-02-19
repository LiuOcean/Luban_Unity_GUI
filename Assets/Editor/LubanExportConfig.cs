using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Luban.Editor
{
    [CreateAssetMenu(fileName = "Luban", menuName = "Luban/ExportConfig")]
    [Serializable]
    public partial class LubanExportConfig : ScriptableObject
    {
        [HideLabel]
        [TabGroup("Split", "预配置项")]
        [FoldoutGroup("Split/预配置项/Luban conf 文件配置")]
        public LubanConfig config;

        [LabelText("键值对")]
        [TabGroup("Split", "预配置项")]
        [FoldoutGroup("Split/预配置项/选单扩展")]
        [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "自定义选单")]
        public CustomDropDownDic dropdown;

        [TabGroup("Split", "预配置项")]
        [FoldoutGroup("Split/预配置项/选单扩展")]
        [Button("重置")]
        public void ResetDropdownDic()
        {
            dropdown ??= new CustomDropDownDic();
            dropdown.Clear();

            dropdown.Add(
                nameof(code_target),
                "cs-bin, cs-simple-json, cs-dotnet-json, cs-editor-json, lua-lua, lua-bin, java-bin, java-json, cpp-bin, go-bin, go-json, python-json, dgscript-json, typescript-json, protobuf2, protobuf3, flatbuffers"
            );

            dropdown.Add(
                nameof(data_target),
                "bin, bin-offset, json, lua, xml, yml, bson, msgpack, protobuf-bin, protobuf-json, flatbuffers-json, text-list"
            );

            dropdown.Add(
                nameof(code_style),
                "csharp-default, java-default, go-default, lua-default, typescript-default, cpp-default, python-default"
            );
            
            dropdown.Add("lineEnding", ",crlf, lf, cr");

            dropdown.Add(nameof(data_exporter), "default");

            dropdown.Add(nameof(output_saver),            "local");
            dropdown.Add(nameof(schema_collector),        "default");
            dropdown.Add(nameof(pipeline),                "default");
            dropdown.Add(nameof(l10n_text_provider_name), "default");
        }

        private void Reset()
        {
            config ??= new LubanConfig();
            config.Reset();
            
            ResetDropdownDic();
        }
    }
}