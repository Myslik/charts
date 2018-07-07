using Notino.Charts.Domain;
using Notino.Charts.Helm;
using Notino.Charts.Kubernetes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetReleasesHandler : IGetReleasesHandler
    {
        private readonly IHelmClient helmClient;
        private readonly IKubernetesClient kubernetesClient;

        public GetReleasesHandler(IHelmClient helmClient, IKubernetesClient kubernetesClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
            this.kubernetesClient = kubernetesClient ?? throw new ArgumentNullException(nameof(kubernetesClient));
        }

        public async Task<IEnumerable<HelmRelease>> HandleAsync(GetReleases query)
        {
            var contexts = await kubernetesClient.GetContexts();
            var releases = new List<HelmRelease>();
            foreach(var context in contexts)
            {
                releases.AddRange(await helmClient.List(context.Name));
            }
            return releases;
        }
    }
}
