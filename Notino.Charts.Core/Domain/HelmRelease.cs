using System;

namespace Notino.Charts.Domain
{
    public class HelmRelease
    {
        public HelmRelease(string name, Chart chart, string cluster)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Chart = chart ?? throw new ArgumentNullException(nameof(chart));
            Cluster = cluster ?? throw new ArgumentNullException(nameof(cluster));
        }

        /// <summary>
        /// Name of this release
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Chart used for this release
        /// </summary>
        public Chart Chart { get; }
        /// <summary>
        /// Name of Kubernetes cluster as in ~/.kube/config
        /// </summary>
        public string Cluster { get; }
    }
}
