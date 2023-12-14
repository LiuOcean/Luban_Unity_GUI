using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CliWrap;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Luban.Editor
{
    public partial class LubanExportConfig
    {
        internal static readonly bool IS_WINDOWS = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        [UsedImplicitly]
        internal static readonly bool IS_MACOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        internal static readonly string BAT_FILE = "gen.bat";

        internal static readonly string LINE_END = IS_WINDOWS ? "^\n" : "\\\n";

        [TabGroup("Split", "预览命令")]
        [BoxGroup("Split/预览命令/Command")]
        [TextArea(5, 50)]
        [HideLabel]
        public string command;

        [SerializeField]
        [HideInInspector]
        private string _command_args;

        [TabGroup("Split", "预览命令")]
        [BoxGroup("Split/预览命令/Command")]
        [Button("预览")]
        public void PreviewCommand()
        {
            var sb = new StringBuilder();

            sb.Append($"{luban_dll} {LINE_END}");
            sb.Append($"--conf {luban_conf_path} {LINE_END}");
            sb.Append($"-t {target} {LINE_END}");

            if(force_load_table_datas)
            {
                sb.Append($"-f {LINE_END}");
            }

            if(verbose)
            {
                sb.Append($"-v {LINE_END}");
            }

            if(validation_fail_as_error)
            {
                sb.Append($"--validationFailAsError {LINE_END}");
            }

            if(!string.IsNullOrEmpty(schema_collector))
            {
                sb.Append($"-s {schema_collector} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(pipeline))
            {
                sb.Append($"-p {pipeline} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(include_tag))
            {
                sb.Append($"-i {include_tag} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(exclude_tag))
            {
                sb.Append($"-e {exclude_tag} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(time_zone))
            {
                sb.Append($"--timeZone {time_zone} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(custom_template_dir))
            {
                sb.Append($"--customTemplateDir {custom_template_dir} {LINE_END}");
            }

            if(output_table is not null && output_table.Count > 0)
            {
                foreach(var table in output_table)
                {
                    if(string.IsNullOrEmpty(table))
                    {
                        continue;
                    }

                    sb.Append($"-o {table} {LINE_END}");
                }
            }

            if(multi_code_target)
            {
                foreach(var target in code_targets)
                {
                    if(string.IsNullOrEmpty(target.code_type))
                    {
                        continue;
                    }

                    sb.Append($"-c {target.code_type} {LINE_END}");
                }
            }
            else if(!string.IsNullOrEmpty(code_target))
            {
                sb.Append($"-c {code_target} {LINE_END}");
            }

            if(multi_data_target)
            {
                foreach(var target in data_targets)
                {
                    if(string.IsNullOrEmpty(target.data_type))
                    {
                        continue;
                    }

                    sb.Append($"-d {target.data_type} {LINE_END}");
                }
            }
            else if(!string.IsNullOrEmpty(data_target))
            {
                sb.Append($"-d {data_target} {LINE_END}");
            }

            if(multi_code_target)
            {
                foreach(var target in code_targets)
                {
                    if(string.IsNullOrEmpty(target.output_dir))
                    {
                        continue;
                    }

                    sb.Append($"-x {target.code_type}.outputCodeDir={target.output_dir} {LINE_END}");
                }
            }
            else if(!string.IsNullOrEmpty(output_code_dir))
            {
                sb.Append($"-x outputCodeDir={output_code_dir} {LINE_END}");
            }

            if(multi_data_target)
            {
                foreach(var target in data_targets)
                {
                    if(string.IsNullOrEmpty(target.output_dir))
                    {
                        continue;
                    }

                    sb.Append($"-x {target.data_type}.outputDataDir={target.output_dir} {LINE_END}");
                }
            }
            else if(!string.IsNullOrEmpty(output_data_dir))
            {
                sb.Append($"-x outputDataDir={output_data_dir} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(code_style))
            {
                sb.Append($"-x codeStyle={code_style} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(data_exporter))
            {
                sb.Append($"-x dataExporter={data_exporter} {LINE_END}");
            }

            if(code_postprocess is not null && code_postprocess.Count > 0)
            {
                sb.Append($"-x codePostprocess={string.Join(",", code_postprocess)}");
            }

            if(data_postprocess is not null && data_postprocess.Count > 0)
            {
                sb.Append($"-x dataPostprocess={string.Join(",", data_postprocess)}");
            }

            if(!string.IsNullOrEmpty(output_saver))
            {
                sb.Append($"-x outputSaver={output_saver} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(l10n_text_provider_name))
            {
                sb.Append($"-x l10n.textProviderName={l10n_text_provider_name} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(l10n_text_provider_file))
            {
                sb.Append($"-x l10n.textProviderFile={l10n_text_provider_file} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(l10n_text_list_file))
            {
                sb.Append($"-x l10n.textListFile={l10n_text_list_file} {LINE_END}");
            }

            if(!string.IsNullOrEmpty(path_validator_root_dir))
            {
                sb.Append($"-x pathValidator.rootDir={path_validator_root_dir} {LINE_END}");
            }

            if(custom_args is not null && custom_args.Count > 0)
            {
                foreach(var args in custom_args)
                {
                    if(string.IsNullOrEmpty(args.key))
                    {
                        continue;
                    }

                    sb.Append(
                        string.IsNullOrEmpty(args.value)
                            ? $"-x {args.key} {LINE_END}"
                            : $"-x {args.key}={args.value} {LINE_END}"
                    );
                }
            }

            _command_args = sb.ToString().TrimEnd(LINE_END.ToCharArray());

            if(IS_WINDOWS)
            {
                dotnet_path = "dotnet";
            }

            command = $"{dotnet_path} {_command_args}";
        }

        [TabGroup("Split", "预览命令")]
        [BoxGroup("Split/预览命令/Command")]
        [Button("执行")]
        public void RunCommand()
        {
            PreviewCommand();

            var full_path = Path.GetFullPath(luban_conf_path);

            var dir = Path.GetDirectoryName(full_path);

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if(File.Exists(full_path))
            {
                File.Delete(full_path);
            }

            config.PrepareJson();

            var json = JsonConvert.SerializeObject(config, Formatting.Indented);

            File.WriteAllText(full_path, json);

            string exe;
            string args;

            if(IS_WINDOWS)
            {
                exe  = "cmd";
                args = $"/c \"{BAT_FILE}\"";

                File.WriteAllText($"./{BAT_FILE}", command);
            }
            else
            {
                exe  = dotnet_path;
                args = _command_args;
            }

            var cli = Cli.Wrap(exe).WithArguments(args).WithWorkingDirectory(".") | (Debug.Log, Debug.LogError);

            try
            {
                cli.ExecuteAsync().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                EditorUtility.DisplayDialog("错误", "请检查日志", "确定");
            }
            finally
            {
                if(File.Exists(BAT_FILE))
                {
                    File.Delete(BAT_FILE);
                }
            }
        }
    }
}
