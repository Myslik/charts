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
        Task<string> InspectValues(string chartName, string version);

        Task Install(string releaseName, string chartName, string version, string kubeContext, string values);
        Task Upgrade(string releaseName, string chartName, string version, string kubeContext, string values);
        Task<string> Get(string releaseName, string kubeContext);
        Task<string> GetValues(string releaseName, string kubeContext);
        Task<string> Status(string releaseName, string kubeContext);
    }
}