using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class GetChartIndexTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_ReturnChartIndex()
        {
            var handler = ServiceProvider.GetService<IGetChartIndexHandler>();
            await handler.HandleAsync(new GetChartIndex());
        }
    }
}
