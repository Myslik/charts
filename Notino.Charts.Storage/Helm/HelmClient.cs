using Notino.Charts.IO;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Charts.Helm
{
    public class HelmClient : IHelmClient
    {
        private readonly IFileSystem fileSystem;
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public HelmClient(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new System.ArgumentNullException(nameof(fileSystem));
        }

        public async Task<string> Index()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var result = await RunProcessAsync("helm", $"repo index {fileSystem.ChartDirectory}");
                if (result.ExitCode != 0)
                {
                    throw new HelmException($"Helm application returned non-zero exit code ({result.ExitCode})");
                }
                return await fileSystem.ReadIndexAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<string> Readme(string chartName, string version)
        {
            var result = await RunProcessAsync("helm", $"inspect readme {fileSystem.ChartDirectory}{chartName}-{version}.tgz");
            if (result.ExitCode != 0)
            {
                throw new HelmException($"Helm application returned non-zero exit code ({result.ExitCode})");
            }
            return result.Output;
        }

        static async Task<ProcessResult> RunProcessAsync(string fileName, string arguments)
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

            return new ProcessResult(exitCode, output.ToString());
        }

        class ProcessResult
        {
            public ProcessResult(int exitCode, string output)
            {
                ExitCode = exitCode;
                Output = output;
            }

            public int ExitCode { get; }
            public string Output { get; }
        }
    }
}
