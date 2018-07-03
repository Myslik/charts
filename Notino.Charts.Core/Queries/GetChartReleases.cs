using Notino.Charts.Domain;
using System;
using System.Collections.Generic;

namespace Notino.Charts.Queries
{
    public class GetChartReleases : IQuery<IEnumerable<ChartRelease>>
    {
        public GetChartReleases(string chartName)
        {
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
        }

        public string ChartName { get; }
    }
}
