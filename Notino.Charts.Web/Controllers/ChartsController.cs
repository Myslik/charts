using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Notino.Charts.Commands;
using Notino.Charts.Commands.Handlers;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Controllers
{
    [Route("[controller]")]
    public class ChartsController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGetChartIndexHandler getChartIndexFile;
        private readonly IUploadChartHandler uploadChart;

        public ChartsController(
            IMemoryCache memoryCache,
            IGetChartIndexHandler getChartIndexFile,
            IUploadChartHandler uploadChart)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.getChartIndexFile = getChartIndexFile ?? throw new ArgumentNullException(nameof(getChartIndexFile));
            this.uploadChart = uploadChart ?? throw new ArgumentNullException(nameof(uploadChart));
        }

        [HttpGet("{filename}")]
        public ActionResult Get(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "charts", filename);
            if (System.IO.File.Exists(path))
            {
                var stream = System.IO.File.OpenRead(path);
                return File(stream, "application/tar");
            }

            return NotFound();
        }

        [HttpGet]
        [HttpGet("index.yaml")]
        public async Task<ActionResult> Get()
        {
            var indexFile = await memoryCache.GetOrCreateAsync("index.yaml", async e =>
            {
                return await getChartIndexFile.HandleAsync(new GetChartIndex());
            });
            return new FileContentResult(Encoding.UTF8.GetBytes(indexFile), "text/yaml");
        }

        [HttpPut("{filename}")]
        public async Task<ActionResult> Upload(string filename)
        {
            await uploadChart.HandleAsync(new UploadChart(filename, Request.Body));
            memoryCache.Remove("index.yaml");

            return Ok();
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
