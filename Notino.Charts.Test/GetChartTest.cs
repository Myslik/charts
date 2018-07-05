using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class GetChartTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_ReturnChart()
        {
            var handler = ServiceProvider.GetService<IGetChartHandler>();
            var chart = await handler.HandleAsync(new GetChart("redis"));
            Assert.NotNull(chart);
        }
    }
}
