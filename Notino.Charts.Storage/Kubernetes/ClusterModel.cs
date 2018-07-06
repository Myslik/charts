namespace Notino.Charts.Kubernetes
{
    public class ClusterModel
    {
        public string Name { get; set; }
        public ClusterInfo Cluster { get; set; }
    }

    public class ClusterInfo
    {
        public string Server { get; set; }
    }
}
