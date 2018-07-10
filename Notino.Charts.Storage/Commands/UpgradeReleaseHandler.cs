using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Commands.Handlers
{
    public class UpgradeReleaseHandler : IUpgradeReleaseHandler
    {
        private readonly IHelmClient helmClient;

        public UpgradeReleaseHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task HandleAsync(UpgradeRelease command)
        {
            await helmClient.Upgrade(command.ReleaseName, command.ChartName, command.Version, command.KubeContext, command.Values);
        }
    }
}
