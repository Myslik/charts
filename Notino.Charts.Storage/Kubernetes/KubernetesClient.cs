using Notino.Charts.Domain;
using Notino.Charts.Runner;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Linq;

namespace Notino.Charts.Kubernetes
{
    public class KubernetesClient : IKubernetesClient
    {
        private readonly IProcessRunner processRunner;

        public KubernetesClient(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public async Task<IEnumerable<KubernetesCluster>> GetClusters()
        {
            var result = await processRunner.RunProcessAsync("kubectl", $"config get-clusters");
            var csv = new CsvReader(new StringReader(result.Output));
            csv.Configuration.RegisterClassMap<ClusterMap>();
            var records = csv.GetRecords<Cluster>();
            return records.Select(c => new KubernetesCluster(c.Name));
        }
    }
}
