using Notino.Charts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Helm
{
    public interface IHelmClient
    {
        Task<string> Index();
        Task<string> Readme(string chartName, string version);
        Task<IEnumerable<HelmRelease>> List(string kubeContext);
        Task Delete(string chartName, string kubeContext);
        Task Install(string chartName, string version, string releaseName, string kubeContext);
    }
}