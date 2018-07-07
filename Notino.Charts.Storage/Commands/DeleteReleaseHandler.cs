using Notino.Charts.Helm;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Commands.Handlers
{
    public class DeleteReleaseHandler : IDeleteReleaseHandler
    {
        private readonly IHelmClient helmClient;

        public DeleteReleaseHandler(
            IHelmClient helmClient)
        {
            this.helmClient = helmClient ?? throw new ArgumentNullException(nameof(helmClient));
        }

        public async Task HandleAsync(DeleteRelease command)
        {
            await helmClient.Delete(command.ChartName, command.KubeContext);
        }
    }
}
