using System;

namespace Notino.Charts.Commands
{
    public class InstallChart : ICommand
    {
        public InstallChart(
            string chartName,
            string version,
            string releaseName,
            string kubeContext,
            string values)
        {
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            ReleaseName = releaseName ?? throw new ArgumentNullException(nameof(releaseName));
            KubeContext = kubeContext ?? throw new ArgumentNullException(nameof(kubeContext));
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public string ChartName { get; }
        public string Version { get; }
        public string ReleaseName { get; }
        public string KubeContext { get; }
        public string Values { get; }
    }
}
