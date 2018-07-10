using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetReleaseValuesHandler : IGetReleaseValuesHandler
    {
        private readonly IHelmClient helmClient;

        public GetReleaseValuesHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetReleaseValues query)
        {
            return await helmClient.GetValues(query.ReleaseName, query.KubeContext);
        }
    }
}
