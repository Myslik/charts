using Microsoft.Extensions.DependencyInjection;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System.Threading.Tasks;
using Xunit;

namespace Notino.Charts.Test
{
    public class GetChartReadmeTest : IntegrationTestContext
    {
        [Fact]
        public async Task Handle_ReturnChartReadme()
        {
            var handler = ServiceProvider.GetService<IGetChartReadmeHandler>();
            var readme = await handler.HandleAsync(new GetChartReadme("redis", new SemVer.Version("3.6.0")));
            Assert.False(string.IsNullOrEmpty(readme));
        }
    }
}
