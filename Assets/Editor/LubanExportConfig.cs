using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Luban.Editor
{
    [CreateAssetMenu(fileName = "Luban", menuName = "Luban/ExportConfig")]
    public class LubanExportConfig : ScriptableObject
    {
        #region 生命周期

        [LabelText("生成前")]
        [ValueDropdown(nameof(_before_gen_dropdowns))]
        [BoxGroup("生命周期")]
        public string before_gen;

        private ValueDropdownList<string> _before_gen_dropdowns => TypeConvert.BEFORE_GENS;

        [LabelText("生成后")]
        [ValueDropdown(nameof(_after_gen_dropdowns))]
        [BoxGroup("生命周期")]
        public string after_gen;

        private ValueDropdownList<string> _after_gen_dropdowns => TypeConvert.AFTER_GENS;

        #endregion

        #region 必要参数

        [Required]
        [LabelText(".Net 路径")]
        [FilePath]
        [BoxGroup("必要参数")]
        public string dotnet_path;

        [Required]
        [LabelText("Client&Server Dll")]
        [FilePath(Extensions = "dll", RequireExistingPath = true)]
        [BoxGroup("必要参数")]
        public string which_dll;

        [Required]
        [Category("-t")]
        [FolderPath(RequireExistingPath = true)]
        [LabelText("模板文件夹")]
        [BoxGroup("必要参数")]
        public string tpl_path;

        [Category("-j")]
        [HideInInspector]
        [BoxGroup("必要参数")]
        public string job = "cfg --";

        [Required]
        [Category("-d")]
        [FilePath(Extensions = "xml")]
        [LabelText("Root.xml")]
        [BoxGroup("必要参数")]
        public string define_xml;

        [Required]
        [Category("--input_data_dir")]
        [LabelText("数据文件夹")]
        [FolderPath]
        [BoxGroup("必要参数")]
        public string input_data_dir;

        [Required]
        [Category("--gen_types")]
        [LabelText("生成类型")]
        [BoxGroup("必要参数")]
        public GenTypes gen_types;

        [Required]
        [Category("-s")]
        [LabelText("生成类型")]
        [Tooltip("一般为 server, client 等")]
        [BoxGroup("必要参数")]
        public string service;

        #endregion

        #region 输出配置

        [Category("--output_data_dir")]
        [LabelText("配置文件夹")]
        [FolderPath]
        [FoldoutGroup("输出配置")]
        public string output_data_dir;

        [Category("--output_code_dir")]
        [LabelText("代码文件夹")]
        [FolderPath]
        [FoldoutGroup("输出配置")]
        public string output_code_dir;

        [Category("--output:data:resource_list_file")]
        [LabelText("资源引用存放文件")]
        [FilePath]
        [FoldoutGroup("输出配置")]
        public string output_data_resources_list_file;

        [Category("--output:exclude_tags")]
        [LabelText("排除哪些 Tag")]
        [FoldoutGroup("输出配置")]
        public string output_exclude_tags;

        [Category("--output:data:file_extension")]
        [LabelText("自定义配置后缀")]
        [FoldoutGroup("输出配置")]
        public string output_data_file_extension;

        [Category("--output:data:compact_json")]
        [LabelText("紧凑 Json")]
        [ShowIf("@_CheckGenType(\"json\")")]
        [FoldoutGroup("输出配置")]
        public bool output_data_compact_json;

        [Category("--output:data:json_monolithic_file")]
        [LabelText("Monolithic Json")]
        [ShowIf("@_CheckGenType(\"monolithic\")")]
        [FoldoutGroup("输出配置")]
        public string output_data_json_monolithic_file;

        private bool _CheckGenType(string which_key_word)
        {
            foreach(GenTypes type in Enum.GetValues(typeof(GenTypes)))
            {
                if(!type.ToString().Contains(which_key_word))
                {
                    continue;
                }

                if(!gen_types.HasFlag(type))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        #endregion

        #region I10N

        [Category("--l10n:timezone")]
        [LabelText("多语言时区")]
        [FoldoutGroup("I10N")]
        public string i10n_timezone;

        [Category("--l10n:input_text_files")]
        [LabelText("多语言文件(','分割)")]
        [FoldoutGroup("I10N")]
        public string i10n_input_text_files;

        [Category("--l10n:text_field_name")]
        [LabelText("默认 Text 属性名")]
        [FoldoutGroup("I10N")]
        public string i10n_text_field_name;

        [Category("--l10n:output_not_translated_text_file")]
        [LabelText("未翻译存放位置")]
        [FilePath]
        [FoldoutGroup("I10N")]
        public string i10n_output_not_translated_text_file;

        [Category("--l10n:patch")]
        [LabelText("多语言补丁")]
        [FoldoutGroup("I10N")]
        public string i10n_path;

        [Category("--l10n:patch_input_data_dir")]
        [LabelText("多语言补丁文件夹")]
        [FolderPath]
        [FoldoutGroup("I10N")]
        [ShowIf("@!string.IsNullOrEmpty(i10n_path)")]
        public string i10n_patch_input_data_dir;

        #endregion

        #region 其他

        [Category("--naming_convention:module")]
        [LabelText("Module 命名风格")]
        [FoldoutGroup("其他配置")]
        public NamingConvertion naming_convertion_module;

        [Category("--naming_convention:bean_member")]
        [LabelText("Bean 命名风格")]
        [FoldoutGroup("其他配置")]
        public NamingConvertion naming_convertion_bean_member;

        [Category("--naming_convention:enum_member")]
        [LabelText("Enum 命名风格")]
        [FoldoutGroup("其他配置")]
        public NamingConvertion naming_convertion_enum_member;

        [Category("--cs:use_unity_vector")]
        [LabelText("使用 Unity Vector")]
        [FoldoutGroup("其他配置")]
        public bool cs_use_unity_vestor;

        [Category("--external:selectors")]
        [LabelText("外部选择器")]
        [FoldoutGroup("其他配置")]
        public string external_selectors;

        #endregion

        [Button("生成")]
        public void Gen()
        {
            StringBuilder sb = new StringBuilder();

            var fields = GetType().GetFields();

            sb.Append(which_dll);

            foreach(var field_info in fields)
            {
                var category = field_info.GetCustomAttribute<CategoryAttribute>();

                if(category is null)
                {
                    continue;
                }

                var value = field_info.GetValue(this).ToString();

                // 当前值为空 或者 False, 或者 None(Enum 默认值)
                // 则继续循环
                if(string.IsNullOrEmpty(value) || string.Equals(value, "False") || string.Equals(value, "None"))
                {
                    continue;
                }

                if(string.Equals(value, "True"))
                {
                    value = string.Empty;
                }

                value = value.Replace(", ", ",");

                sb.Append($" {category.Category} {value} ");
            }

            GenUtils.Gen(
                dotnet_path,
                sb.ToString(),
                before_gen,
                after_gen
            );
        }
    }
}