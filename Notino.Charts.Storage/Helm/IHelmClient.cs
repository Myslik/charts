using Notino.Charts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Helm
{
    public interface IHelmClient
    {
        Task<string> Index();
        Task<IEnumerable<HelmRelease>> List();
        Task<string> Readme(string chartName, string version);
    }
}