using Notino.Charts.Helm;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetChartIndexHandler : IGetChartIndexHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartIndexHandler(IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new System.ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetChartIndex query)
        {
            return await helmClient.Index();
        }
    }
}
