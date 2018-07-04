using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Queries;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class GetChartIndexFileTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_ReturnChartIndexFile()
        {
            var handler = ServiceProvider.GetService<IGetChartIndexYamlHandler>();
            await handler.HandleAsync(new GetChartIndexYaml());
        }
    }
}
