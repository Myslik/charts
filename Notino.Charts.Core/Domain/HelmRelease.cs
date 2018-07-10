using System;

namespace Notino.Charts.Domain
{
    public class HelmRelease
    {
        public HelmRelease(
            string name,
            string chart,
            string cluster,
            int revision,
            string status)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ChartName = chart.Substring(0, chart.LastIndexOf("-"));
            ChartVersion = chart.Substring(chart.LastIndexOf("-") + 1);
            Cluster = cluster ?? throw new ArgumentNullException(nameof(cluster));
            Revision = revision;
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }

        /// <summary>
        /// Name of this release
        /// </summary>
        public string Name { get; }
        public string ChartName { get; }
        public string ChartVersion { get; }
        /// <summary>
        /// Name of Kubernetes cluster as in ~/.kube/config
        /// </summary>
        public string Cluster { get; }
        public int Revision { get; }
        public string Status { get; }
    }
}
