using System;

namespace Notino.Charts.Queries
{
    public class GetReleaseDetails : IQuery<string>
    {
        public GetReleaseDetails(string releaseName, string kubeContext)
        {
            ReleaseName = releaseName ?? throw new ArgumentNullException(nameof(releaseName));
            KubeContext = kubeContext ?? throw new ArgumentNullException(nameof(kubeContext));
        }

        public string ReleaseName { get; }
        public string KubeContext { get; }
    }
}
