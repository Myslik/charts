using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Commands.Handlers
{
    public class InstallChartHandler : IInstallChartHandler
    {
        private readonly IHelmClient helmClient;

        public InstallChartHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task HandleAsync(InstallChart command)
        {
            await helmClient.Install(command.ChartName, command.Version, command.ReleaseName, command.KubeContext, command.Values);
        }
    }
}
