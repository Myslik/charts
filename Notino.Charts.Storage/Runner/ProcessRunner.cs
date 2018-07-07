using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Charts.Runner
{
    public class ProcessRunner : IProcessRunner
    {
        public async Task<ProcessResult> RunProcessAsync(string fileName, string arguments)
        {
            var tcs = new TaskCompletionSource<int>();
            var output = new StringBuilder();

            var process = new Process
            {
                StartInfo = { FileName = fileName, Arguments = arguments, UseShellExecute = false, RedirectStandardOutput = true },
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (sender, args) =>
            {
                output.AppendLine(args.Data);
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();
            process.BeginOutputReadLine();

            var exitCode = await tcs.Task;

            if (exitCode != 0)
            {
                throw new ProcessRunnerException($"Process {fileName} returned non-zero exit code ({exitCode})");
            }

            return new ProcessResult(exitCode, output.ToString());
        }
    }
}
