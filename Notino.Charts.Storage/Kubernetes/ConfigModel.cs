using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Notino.Charts.Kubernetes
{
    public class ConfigModel
    {
        public string ApiVersion { get; set; }
        public IEnumerable<ClusterModel> Clusters { get; set; }
        public IEnumerable<ContextModel> Contexts { get; set; }
        [YamlMember(Alias = "current-context", ApplyNamingConventions = false)]
        public string CurrentContext { get; set; }
        public string Kind { get; set; }
    }
}
