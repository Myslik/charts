namespace Notino.Charts.Kubernetes
{
    public class ContextModel
    {
        public string Name { get; set; }
        public ContextInfo Context { get; set; }
    }

    public class ContextInfo
    {
        public string Cluster { get; set; }
        public string User { get; set; }
    }
}
