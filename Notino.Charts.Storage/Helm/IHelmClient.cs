using System.Threading.Tasks;

namespace Notino.Charts.Helm
{
    public interface IHelmClient
    {
        Task<string> Index();
        Task<string> Readme(string chartName, string version);
    }
}