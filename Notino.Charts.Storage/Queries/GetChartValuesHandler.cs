using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetChartValuesHandler : IGetChartValuesHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartValuesHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetChartValues query)
        {
            return await helmClient.InspectValues(query.ChartName, query.Version);
        }
    }
}
