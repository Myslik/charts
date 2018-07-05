using CsvHelper.Configuration;

namespace Notino.Charts.Kubernetes
{
    internal class Cluster
    {
        public string Name { get; set; }
    }

    internal sealed class ClusterMap : ClassMap<Cluster>
    {
        public ClusterMap()
        {
            Map(m => m.Name).Name("NAME");
        }
    }
}
