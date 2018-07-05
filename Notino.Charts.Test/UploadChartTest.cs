using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Commands;
using Notino.Charts.Commands.Handlers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class UploadChartTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_Success()
        {
            var handler = ServiceProvider.GetService<IUploadChartHandler>();
            await handler.HandleAsync(new UploadChart("redis-3.6.0.tgz", File.OpenRead("files\\redis-3.6.0.tgz")));
        }
    }
}
