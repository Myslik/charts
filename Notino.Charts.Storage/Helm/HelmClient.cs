using Notino.Charts.Domain;
using Notino.Charts.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Notino.Charts.Helm
{
    public class HelmClient : IHelmClient
    {
        private readonly IFileSystem fileSystem;

        public HelmClient(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new System.ArgumentNullException(nameof(fileSystem));
        }

        public async Task<string> Index()
        {
            var exitCode = await RunProcessAsync("helm", $"repo index {fileSystem.ChartDirectory}");
            if (exitCode != 0)
            {
                throw new HelmException($"Helm application returned non-zero exit code ({exitCode})");
            }
            return await fileSystem.ReadIndexAsync();
        }

        public async Task<IEnumerable<Chart>> GetCharts()
        {
            var input = new StringReader(await Index());

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var index = deserializer.Deserialize<HelmIndex>(input);

            if (index.ApiVersion != "v1")
            {
                throw new HelmException("Only v1 apiVersion is supported");
            }

            return index.Entries.Select(kv =>
            {
                return new Chart(kv.Key) { Description = kv.Value.Last()?.Description };
            });
        }

        static Task<int> RunProcessAsync(string fileName, string arguments)
        {
            var tcs = new TaskCompletionSource<int>();

            var process = new Process
            {
                StartInfo = { FileName = fileName, Arguments = arguments },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }

        class HelmIndex
        {
            public string ApiVersion { get; set; }
            public Dictionary<string, IEnumerable<HelmEntry>> Entries { get; set; }
        }

        class HelmEntry
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Description { get; set; }
        }
    }
}
