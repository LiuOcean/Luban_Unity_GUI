using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Luban.Editor
{
    internal static class GenUtils
    {
        internal static readonly string _DOTNET =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "dotnet.exe" : "dotnet";

        public static void Gen(string arguments, string before, string after)
        {
            Debug.Log(arguments);

            IBeforeGen before_gen = null;

            if(!string.IsNullOrEmpty(before))
            {
                var type = Type.GetType(before);

                if(type != null)
                {
                    before_gen = Activator.CreateInstance(type) as IBeforeGen;
                }
            }

            IAfterGen after_gen = null;

            if(!string.IsNullOrEmpty(after))
            {
                var type = Type.GetType(after);

                if(type != null)
                {
                    after_gen = Activator.CreateInstance(type) as IAfterGen;
                }
            }

            before_gen?.Process();

            var process = _Run(
                _DOTNET,
                arguments,
                ".",
                true
            );

            Debug.Log(process.StandardOutput.ReadToEnd());

            after_gen?.Process();

            AssetDatabase.Refresh();
        }

        private static Process _Run(string exe,
                                    string arguments,
                                    string working_dir = ".",
                                    bool   wait_exit   = false)
        {
            try
            {
                bool redirect_standard_output = true;
                bool redirect_standard_error  = true;
                bool use_shell_execute        = false;

                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    redirect_standard_output = false;
                    redirect_standard_error  = false;
                    use_shell_execute        = true;
                }

                if(wait_exit)
                {
                    redirect_standard_output = true;
                    redirect_standard_error  = true;
                    use_shell_execute        = false;
                }

                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName               = exe,
                    Arguments              = arguments,
                    CreateNoWindow         = true,
                    UseShellExecute        = use_shell_execute,
                    WorkingDirectory       = working_dir,
                    RedirectStandardOutput = redirect_standard_output,
                    RedirectStandardError  = redirect_standard_error,
                };

                Process process = Process.Start(info);

                if(wait_exit)
                {
                    WaitForExitAsync(process).ConfigureAwait(false);
                }

                return process;
            }
            catch(Exception e)
            {
                throw new Exception($"dir: {Path.GetFullPath(working_dir)}, command: {exe} {arguments}", e);
            }
        }

        private static async Task WaitForExitAsync(this Process self)
        {
            if(!self.HasExited)
            {
                return;
            }

            try
            {
                self.EnableRaisingEvents = true;
            }
            catch(InvalidOperationException)
            {
                if(self.HasExited)
                {
                    return;
                }

                throw;
            }

            var tcs = new TaskCompletionSource<bool>();

            void Handler(object s, EventArgs e) => tcs.TrySetResult(true);

            self.Exited += Handler;

            try
            {
                if(self.HasExited)
                {
                    return;
                }

                await tcs.Task;
            }
            finally
            {
                self.Exited -= Handler;
            }
        }
    }
}