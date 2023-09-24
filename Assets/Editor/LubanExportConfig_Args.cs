using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Luban.Editor
{
    public partial class LubanExportConfig
    {
        
        [Required]
        [LabelText("dotnet 路径")]
        [TabGroup("Split", "参数配置")]
        [BoxGroup("Split/参数配置/必要参数")]
        [ShowIf("IS_MACOS")]
        public string dotnet_path;

        [Required]
        [LabelText("生成目标")]
        [TabGroup("Split", "参数配置")]
        [BoxGroup("Split/参数配置/必要参数")]
        [ValueDropdown("@config.GetTargetDropdown()")]
        public string target;

        [Required]
        [LabelText("luban 配置项")]
        [TabGroup("Split", "参数配置")]
        [BoxGroup("Split/参数配置/必要参数")]
        public string luban_conf_path;

        [Required]
        [LabelText("luban dll")]
        [TabGroup("Split", "参数配置")]
        [BoxGroup("Split/参数配置/必要参数")]
        [FilePath(Extensions = "dll", RequireExistingPath = true)]
        public string luban_dll;

        [LabelText("强行加载配置数据")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("即使没有指定任何dataTarget也要强行加载配置数据，适用于在配置表提交前检查配置合法性")]
        public bool force_load_table_datas;

        [LabelText("详细日志")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        public bool verbose;

        [LabelText("严格校验")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("如果有任何校验器未通过，则生成失败。此参数一般在正式发布时使用")]
        public bool validation_fail_as_error;

        [LabelText("Schema 根收集器")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"schema_collector\", true)")]
        public string schema_collector;

        [LabelText("生成管线")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"pipeline\", true)")]
        public string pipeline;

        [LabelText("包含的 tag")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("包含该tag的记录会被输出到数据目标")]
        public string include_tag;

        [LabelText("排除的 tag")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("包含该tag的记录不会被输出到数据目标")]
        public string exclude_tag;

        [LabelText("时区")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("指定当前时区，默认取本地时区。此参数会影响datetime类型。该参数为linux或win下的时区名，例如 Asia/Shanghai 或 China Standard Time")]
        [ValueDropdown("@_GetAllTimeZone()")]
        public string time_zone;

        [FolderPath]
        [LabelText("自定义模板目录")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("自定义template搜索路径，优先级搜索此路径，再搜索默认的Templates路径")]
        public string custom_template_dir;

        [LabelText("要生成的 Table")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数")]
        [Tooltip("指定要生成的table，可以有多个，例如-o item.tbItem -o bag.TbBag。如果未指定此参数，则按照group规则计算导出的table列表")]
        public List<string> output_table;

        [LabelText("多代码生成开关")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/代码生成")]
        public bool multi_code_target;

        [LabelText("代码生成")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/代码生成")]
        [HideIf(nameof(multi_code_target))]
        [ValueDropdown("@dropdown.GetDropdown(\"code_target\", false)")]
        public string code_target;

        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/代码生成")]
        [Tooltip("可以有0-n个。如 -c cs-bin -c java-json")]
        [ShowIf(nameof(multi_code_target))]
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        [HideLabel]
        public List<CodeTarget> code_targets;

        [LabelText("多数据生成开关")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/数据生成")]
        public bool multi_data_target;

        [LabelText("数据生成")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/数据生成")]
        [HideIf(nameof(multi_data_target))]
        [ValueDropdown("@dropdown.GetDropdown(\"data_target\", false)")]
        public string data_target;

        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/缺省参数/数据生成")]
        [Tooltip("可以有0-n个。如 -d bin -d json")]
        [ShowIf(nameof(multi_data_target))]
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
        [HideLabel]
        public List<DataTarget> data_targets;

        [LabelText("代码输出目录")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [HideIf(nameof(multi_code_target))]
        public string output_code_dir;

        [LabelText("数据输出目录")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [HideIf(nameof(multi_data_target))]
        public string output_data_dir;

        [LabelText("命名风格")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"code_style\", true)")]
        public string code_style;

        [LabelText("数据导出器")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"data_exporter\", true)")]
        public string data_exporter;

        [LabelText("数据保存器")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"output_saver\", true)")]
        public string output_saver;

        [LabelText("本地化提供器")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        [ValueDropdown("@dropdown.GetDropdown(\"l10n_text_provider_name\", true)")]
        public string l10n_text_provider_name;

        [LabelText("本地化校验文件")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        public string l10n_text_provider_file;

        [LabelText("Text key 输出文件")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        public string l10n_text_list_file;

        [LabelText("Path校验器根目录")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        public string path_validator_root_dir;


        [LabelText("代码后处理")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        public List<string> code_postprocess;

        [LabelText("数据后处理")]
        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/默认额外参数")]
        public List<string> data_postprocess;

        [TabGroup("Split", "参数配置")]
        [FoldoutGroup("Split/参数配置/用户自定义额外参数")]
        [TableList(ShowIndexLabels = true)]
        [HideLabel]
        public List<CustomXargs> custom_args;

        private ValueDropdownList<string> _GetAllTimeZone()
        {
            ValueDropdownList<string> result = new();

            foreach(var info in TimeZoneInfo.GetSystemTimeZones())
            {
                result.Add(new ValueDropdownItem<string>(info.Id, info.Id));
            }

            result.Sort((x, y) => string.Compare(x.Text, y.Text, StringComparison.Ordinal));

            result.Insert(0, new ValueDropdownItem<string>("无", ""));

            return result;
        }
    }
}