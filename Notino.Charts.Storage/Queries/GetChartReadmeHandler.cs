using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Queries
{
    public class GetChartReadmeHandler : IGetChartReadmeHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartReadmeHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetChartReadme query)
        {
            return await helmClient.Readme(query.ChartName, query.Version.ToString());
        }
    }
}
