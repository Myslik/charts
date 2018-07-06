using Notino.Charts.Domain;
using Notino.Charts.Helm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetReleasesHandler : IGetReleasesHandler
    {
        private readonly IHelmClient helmClient;

        public GetReleasesHandler(IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task<IEnumerable<HelmRelease>> HandleAsync(GetReleases query)
        {
            return await helmClient.List();
        }
    }
}
