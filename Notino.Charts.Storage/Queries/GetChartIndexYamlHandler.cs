using Notino.Charts.Helm;
using System.Threading.Tasks;

namespace Notino.Charts.Queries
{
    public class GetChartIndexYamlHandler : IGetChartIndexYamlHandler
    {
        private readonly IHelmClient helmClient;

        public GetChartIndexYamlHandler(IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new System.ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetChartIndexYaml query)
        {
            return await helmClient.Index();
        }
    }
}
