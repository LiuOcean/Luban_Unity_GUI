using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Luban.Editor
{
    [Serializable]
    public class LubanConfig
    {
        [Required]
        [JsonProperty("dataDir")]
        [LabelText("数据根目录")]
        [FolderPath]
        public string data_dir;

        [Space]
        [TableList(ShowIndexLabels = false)]
        [LabelText("组配置")]
        [JsonProperty]
        public List<RawGroup> groups;

        [JsonProperty("schemaFiles")]
        [TableList(ShowIndexLabels = false)]
        [LabelText("Schema")]
        public List<SchemaFileInfo> schema_files;

        [LabelText("导出目标")]
        [TableList(ShowIndexLabels = false)]
        [JsonProperty]
        public List<RawTarget> targets;

        [Button("重置")]
        public void Reset()
        {
            groups = new List<RawGroup>
            {
                new() {names = "c", is_default = true},
                new() {names = "s", is_default = true},
                new() {names = "e", is_default = true}
            };

            schema_files = new List<SchemaFileInfo>
            {
                new() {file_name = "Defines", type          = ""},
                new() {file_name = "Datas/Table.xlsx", type = "table"},
                new() {file_name = "Datas/Beans.xlsx", type = "bean"},
                new() {file_name = "Datas/Enums.xlsx", type = "enum"}
            };

            targets = new List<RawTarget>
            {
                new() {name = "server", manager = "Tables", groups = "s", top_module       = ""},
                new() {name = "client", manager = "Tables", groups = "c", top_module       = ""},
                new() {name = "all", manager    = "Tables", groups = "c, s, e", top_module = ""}
            };

            data_dir = "Datas";
        }

        public void PrepareJson()
        {
            foreach(var group in groups)
            {
                group.PrepareJson();
            }

            foreach(var target in targets)
            {
                target.PrepareJson();
            }
        }

        public IEnumerable<ValueDropdownItem<string>> GetTargetDropdown()
        {
            foreach(var target in targets)
            {
                yield return new ValueDropdownItem<string>(target.name, target.name);
            }
        }
    }

    [Serializable]
    public class RawGroup
    {
        [JsonIgnore]
        [HideLabel]
        [VerticalGroup("分组名")]
        public string names;

        [JsonProperty("default")]
        [HideLabel]
        [VerticalGroup("是否默认")]
        public bool is_default = true;

        [JsonProperty("names")] private string[] _names { get; set; }

        public void PrepareJson() { _names = names.Replace(" ", "").Split(","); }
    }

    [Serializable]
    public class SchemaFileInfo
    {
        [JsonProperty("fileName")]
        [VerticalGroup("路径")]
        [HideLabel]
        public string file_name;

        [ValueDropdown(nameof(_DROPDOWN))]
        [JsonProperty]
        [VerticalGroup("类型")]
        [HideLabel]
        public string type;

        private static ValueDropdownList<string> _DROPDOWN = new()
        {
            new ValueDropdownItem<string>("无",  ""),
            new ValueDropdownItem<string>("表",  "table"),
            new ValueDropdownItem<string>("对象", "bean"),
            new ValueDropdownItem<string>("枚举", "enum")
        };
    }

    [Serializable]
    public class RawTarget
    {
        [JsonProperty]
        [VerticalGroup("目标名")]
        [HideLabel]
        public string name;

        [JsonProperty]
        [VerticalGroup("管理类名")]
        [HideLabel]
        public string manager = "Tables";

        [JsonIgnore]
        [VerticalGroup("分组")]
        [HideLabel]
        public string groups;

        [JsonProperty("groups")] private string[] _groups { get; set; }

        [JsonProperty]
        [VerticalGroup("命名空间")]
        [HideLabel]
        public string top_module;

        public void PrepareJson() { _groups = groups.Replace(" ", "").Split(","); }
    }
}