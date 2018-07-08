using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Charts
{
    public class DetailModel : PageModel
    {
        private readonly IGetChartHandler getChartHandler;
        private readonly IGetChartReadmeHandler getChartReadmeHandler;

        public DetailModel(
            IGetChartHandler getChartHandler,
            IGetChartReadmeHandler getChartReadmeHandler)
        {
            this.getChartHandler = getChartHandler ?? throw new ArgumentNullException(nameof(getChartHandler));
            this.getChartReadmeHandler = getChartReadmeHandler ?? throw new ArgumentNullException(nameof(getChartReadmeHandler));
        }

        public Chart Chart { get; private set; }

        public ChartRelease Release { get; private set; }

        public string Readme { get; private set; }

        public async Task<IActionResult> OnGet(string name, string version)
        {
            Chart = await getChartHandler.HandleAsync(new GetChart(name));
            if (version == null)
            {
                Release = Chart.GetLastRelease();
            }
            else
            {
                Release = Chart.Releases.SingleOrDefault(c => c.Version == new SemVer.Version(version));
            }
            if (Release == null)
            {
                return NotFound();
            }
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            Readme = Markdown.ToHtml(await getChartReadmeHandler.HandleAsync(new GetChartReadme(Chart.Name, Chart.GetLastRelease()?.Version)), pipeline);
            return Page();
        }
    }
}