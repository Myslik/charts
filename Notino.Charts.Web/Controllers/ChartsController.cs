using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Notino.Charts.Commands;
using Notino.Charts.Queries;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Controllers
{
    [Route("[controller]")]
    public class ChartsController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGetChartIndexYamlHandler getChartIndexFile;
        private readonly IUploadChartHandler uploadChart;

        public ChartsController(
            IMemoryCache memoryCache,
            IGetChartIndexYamlHandler getChartIndexFile,
            IUploadChartHandler uploadChart)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.getChartIndexFile = getChartIndexFile ?? throw new ArgumentNullException(nameof(getChartIndexFile));
            this.uploadChart = uploadChart ?? throw new ArgumentNullException(nameof(uploadChart));
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var indexFile = await memoryCache.GetOrCreateAsync("index.yaml", async e =>
            {
                return await getChartIndexFile.HandleAsync(new GetChartIndexYaml());
            });
            return new FileContentResult(Encoding.UTF8.GetBytes(indexFile), "text/yaml");
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            await uploadChart.HandleAsync(new UploadChart(file.FileName, file.OpenReadStream()));
            memoryCache.Remove("index.yaml");

            return Ok();
        }
    }
}
