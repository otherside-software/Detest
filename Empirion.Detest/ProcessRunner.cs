using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public static class ProcessRunner
    {
        public const string NODE_PATH = "node";

        public static Task<ProcessResult> RunProcessAsync(string processFile, string arguments, IProgress<string> progress)
        {
            var output = new List<string>();
            var errors = new List<string>();

            Process process = CreateProcess(processFile, arguments, output, errors, progress);

            process.Start();
            process.BeginOutputReadLine();

            var tcs = new TaskCompletionSource<ProcessResult>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(new ProcessResult(output, errors));

            return tcs.Task;
        }

        public static ProcessResult RunProcess(string processFile, string arguments)
        {
            var output = new List<string>();
            var errors = new List<string>();

            Process process = CreateProcess(processFile, arguments, output, errors, null);
            
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            //TODO: error -> throw

            return new ProcessResult(output, errors);
        }

        public static ProcessResult RunAnalyzer(string analyzer, string fileToAnalyze)
        {
            return RunProcess(
                NODE_PATH,
                String.Format(
                    "\"{0}\" \"{1}\"",
                    analyzer,
                    fileToAnalyze
                )
            );
        }

        private static Process CreateProcess(string processFile, string arguments, List<string> output, List<string> errors, IProgress<string> progress)
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = false;
            process.EnableRaisingEvents = true;

            process.StartInfo.FileName = processFile;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    output.Add(e.Data);
                    if (progress != null)
                    {
                        progress.Report(e.Data);
                    }
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    errors.Add(e.Data);
                }
            };
            return process;
        }
    }
}
