using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class GetChartsTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_ReturnCharts()
        {
            var handler = ServiceProvider.GetService<IGetChartsHandler>();
            var charts = await handler.HandleAsync(new GetCharts());
            Assert.Equal(Directory.GetFiles("charts\\", "*.tgz").Length, charts.Count());
        }
    }
}
