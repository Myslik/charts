using Notino.Charts.Domain;
using Notino.Charts.Kubernetes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Queries.Handlers
{
    public class GetClustersHandler : IGetClustersHandler
    {
        private readonly IKubernetesClient kubernetesClient;

        public GetClustersHandler(
            IKubernetesClient kubernetesClient)
        {
            this.kubernetesClient = kubernetesClient ?? throw new ArgumentNullException(nameof(kubernetesClient));
        }

        public async Task<IEnumerable<KubernetesCluster>> HandleAsync(GetClusters query)
        {
            return await kubernetesClient.GetClusters();
        }
    }
}
