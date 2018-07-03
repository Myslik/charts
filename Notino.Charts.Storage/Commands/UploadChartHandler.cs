using Notino.Charts.IO;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Commands
{
    public class UploadChartHandler : IUploadChartHandler
    {
        private readonly IFileSystem fileSystem;

        public UploadChartHandler(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public async Task HandleAsync(UploadChart command)
        {
            await fileSystem.SaveAsync(command.Name, command.Stream);
        }
    }
}
