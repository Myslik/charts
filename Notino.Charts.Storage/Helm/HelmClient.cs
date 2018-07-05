using CsvHelper;
using Notino.Charts.Domain;
using Notino.Charts.IO;
using Notino.Charts.Runner;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Charts.Helm
{
    public class HelmClient : IHelmClient
    {
        private readonly IFileSystem fileSystem;
        private readonly IProcessRunner processRunner;
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public HelmClient(
            IFileSystem fileSystem,
            IProcessRunner processRunner)
        {
            this.fileSystem = fileSystem ?? throw new System.ArgumentNullException(nameof(fileSystem));
            this.processRunner = processRunner ?? throw new System.ArgumentNullException(nameof(processRunner));
        }

        public async Task<string> Index()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var result = await processRunner.RunProcessAsync("helm", $"repo index {fileSystem.ChartDirectory}");
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

        public async Task<IEnumerable<HelmRelease>> List()
        {
            var result = await processRunner.RunProcessAsync("helm", $"ls");
            var csv = new CsvReader(new StringReader(result.Output));
            csv.Configuration.RegisterClassMap<ReleaseMap>();
            var records = csv.GetRecords<Release>();
            return records.Select(c => new HelmRelease(c.Name, new Chart(c.Chart), "default"));
        }

        public async Task<string> Readme(string chartName, string version)
        {
            var result = await processRunner.RunProcessAsync("helm", $"inspect readme {fileSystem.ChartDirectory}{chartName}-{version}.tgz");
            if (result.ExitCode != 0)
            {
                throw new HelmException($"Helm application returned non-zero exit code ({result.ExitCode})");
            }
            return result.Output;
        }
    }
}
