namespace Notino.Charts.Queries
{
    public class GetReleaseValues : IQuery<string>
    {
        public GetReleaseValues(
            string releaseName,
            string kubeContext)
        {
            ReleaseName = releaseName ?? throw new System.ArgumentNullException(nameof(releaseName));
            KubeContext = kubeContext ?? throw new System.ArgumentNullException(nameof(kubeContext));
        }

        public string ReleaseName { get; }
        public string KubeContext { get; }
    }
}
