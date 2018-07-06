using System.Collections.Generic;
using System.Threading.Tasks;
using Notino.Charts.Domain;

namespace Notino.Charts.Kubernetes
{
    public interface IKubernetesClient
    {
        Task<IEnumerable<KubernetesContext>> GetContexts();
    }
}