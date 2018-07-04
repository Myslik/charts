using System;

namespace Notino.Charts.Queries
{
    public class GetChartReadme : IQuery<string>
    {
        public GetChartReadme(string chartName, SemVer.Version version)
        {
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }

        public string ChartName { get; }
        public SemVer.Version Version { get; }
    }
}
