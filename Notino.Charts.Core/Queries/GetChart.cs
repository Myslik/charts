using Notino.Charts.Domain;
using System;
using System.Collections.Generic;

namespace Notino.Charts.Queries
{
    public class GetChart : IQuery<Chart>
    {
        public GetChart(string chartName)
        {
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
        }

        public string ChartName { get; }
    }
}
