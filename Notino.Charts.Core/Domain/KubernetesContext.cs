using System;

namespace Notino.Charts.Domain
{
    public class KubernetesContext
    {
        public KubernetesContext(string name, string server)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public string Name { get; }
        public string Server { get; }
    }
}
