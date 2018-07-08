using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetReleaseDetailsHandler : IGetReleaseDetailsHandler
    {
        private readonly IHelmClient helmClient;

        public GetReleaseDetailsHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<string> HandleAsync(GetReleaseDetails query)
        {
            return await helmClient.Get(query.ReleaseName, query.KubeContext);
        }
    }
}
