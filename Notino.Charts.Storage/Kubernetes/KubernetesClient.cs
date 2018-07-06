using Notino.Charts.Domain;
using Notino.Charts.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Notino.Charts.Kubernetes
{
    public class KubernetesClient : IKubernetesClient
    {
        private readonly IProcessRunner processRunner;

        public KubernetesClient(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public async Task<IEnumerable<KubernetesContext>> GetContexts()
        {
            var result = await processRunner.RunProcessAsync("kubectl", $"config view");

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var config = deserializer.Deserialize<ConfigModel>(result.Output);

            return config.Contexts.Select(c => new KubernetesContext(c.Name, config.Clusters.Single(k => k.Name == c.Context.Cluster).Cluster.Server));
        }
    }
}
