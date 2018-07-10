using System;

namespace Notino.Charts.Commands
{
    public class UpgradeRelease : ICommand
    {
        public UpgradeRelease(
            string releaseName,
            string chartName,
            string version,
            string kubeContext,
            string values)
        {
            ReleaseName = releaseName ?? throw new ArgumentNullException(nameof(releaseName));
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            KubeContext = kubeContext ?? throw new ArgumentNullException(nameof(kubeContext));
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public string ReleaseName { get; }
        public string ChartName { get; }
        public string Version { get; }
        public string KubeContext { get; }
        public string Values { get; }
    }
}
