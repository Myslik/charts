using Notino.Charts.Domain;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Notino.Charts.Helm
{
    public static class ChartExtensions
    {
        public static IEnumerable<Chart> GetCharts(string chartYaml)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var index = deserializer.Deserialize<HelmIndex>(chartYaml);

            if (index.ApiVersion != "v1")
            {
                throw new HelmException("Only v1 apiVersion is supported");
            }

            return index.Entries.Select(kv =>
            {
                var chart = new Chart(kv.Key);
                chart.Releases.AddRange(
                    kv.Value.Select(e => new ChartRelease(
                        chart,
                        new SemVer.Version(e.Version),
                        e.Description,
                        e.Home,
                        e.Icon)));
                return chart;
            });
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
            public string Home { get; set; }
            public string Icon { get; set; }
        }
    }
}
