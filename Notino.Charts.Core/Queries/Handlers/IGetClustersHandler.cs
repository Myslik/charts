using Notino.Charts.Domain;
using System.Collections.Generic;

namespace Notino.Charts.Queries.Handlers
{
    public interface IGetClustersHandler : IQueryHandler<GetClusters, IEnumerable<KubernetesCluster>>
    {
    }
}
