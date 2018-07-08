using CsvHelper;
using Notino.Charts.Domain;
using Notino.Charts.IO;
using Notino.Charts.Runner;
using System;
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
                return await fileSystem.ReadIndexAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<string> Readme(string chartName, string version)
        {
            var result = await processRunner.RunProcessAsync("helm", $"inspect readme {fileSystem.ChartDirectory}{chartName}-{version}.tgz");
            return result.Output;
        }

        public async Task<IEnumerable<HelmRelease>> List(string kubeContext)
        {
            var result = await processRunner.RunProcessAsync("helm", $"ls --kube-context {kubeContext}");
            var csv = new CsvReader(new StringReader(result.Output));
            csv.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
            csv.Configuration.Delimiter = "\t";
            csv.Configuration.RegisterClassMap<ReleaseMap>();
            var records = csv.GetRecords<Release>();
            return records.Select(c =>
                new HelmRelease(
                    c.Name,
                    new Chart(c.Chart),
                    kubeContext,
                    c.Revision,
                    c.Status));
        }

        public async Task Delete(string chartName, string kubeContext)
        {
            await processRunner.RunProcessAsync("helm", $"delete --purge {chartName} --kube-context {kubeContext}");
        }

        public async Task Install(string chartName, string version, string releaseName, string kubeContext, string values)
        {
            var valuesFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".yaml";
            File.WriteAllText(valuesFile, values);
            try
            {
                await processRunner.RunProcessAsync("helm", $"install {fileSystem.ChartDirectory}{chartName}-{version}.tgz --name {releaseName} --kube-context {kubeContext} -f {valuesFile}");
            }
            finally
            {
                File.Delete(valuesFile);
            }
        }

        public async Task<string> Get(string releaseName, string kubeContext)
        {
            var result = await processRunner.RunProcessAsync("helm", $"get {releaseName} --kube-context {kubeContext}");
            return result.Output;
        }

        public async Task<string> InspectValues(string chartName, string version)
        {
            var result = await processRunner.RunProcessAsync("helm", $"inspect values {fileSystem.ChartDirectory}{chartName}-{version}.tgz");
            return result.Output;
        }
    }
}
