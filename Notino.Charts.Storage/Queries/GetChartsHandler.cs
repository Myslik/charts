using Notino.Charts.Domain;
using Notino.Charts.Helm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Queries
{
    public class GetChartsHandler : IGetChartsHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartsHandler(IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<IEnumerable<Chart>> HandleAsync(GetCharts query)
        {
            return await helmClient.GetCharts();
        }
    }
}
