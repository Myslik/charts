using System;

namespace Notino.Charts.Domain
{
    public class KubernetesCluster
    {
        public KubernetesCluster(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}
