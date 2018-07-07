using System;

namespace Notino.Charts.Commands
{
    public class DeleteRelease : ICommand
    {
        public DeleteRelease(string chartName, string kubeContext)
        {
            ChartName = chartName ?? throw new ArgumentNullException(nameof(chartName));
            KubeContext = kubeContext ?? throw new ArgumentNullException(nameof(kubeContext));
        }

        public string ChartName { get; }
        public string KubeContext { get; }
    }
}
