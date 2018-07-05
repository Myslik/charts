using Notino.Charts.Domain;
using Notino.Charts.Helm;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetChartHandler : IGetChartHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartHandler(IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<Chart> HandleAsync(GetChart query)
        {
            var charts = ChartExtensions.GetCharts(await helmClient.Index());
            return charts.Single(c => c.Name == query.ChartName);
        }
    }
}
