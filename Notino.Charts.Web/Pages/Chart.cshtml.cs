using Markdig;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages
{
    public class ChartModel : PageModel
    {
        private readonly IGetChartHandler getChartHandler;
        private readonly IGetChartReadmeHandler getChartReadmeHandler;

        public ChartModel(
            IGetChartHandler getChartHandler,
            IGetChartReadmeHandler getChartReadmeHandler)
        {
            this.getChartHandler = getChartHandler ?? throw new ArgumentNullException(nameof(getChartHandler));
            this.getChartReadmeHandler = getChartReadmeHandler ?? throw new ArgumentNullException(nameof(getChartReadmeHandler));
        }

        public Chart Chart { get; private set; }

        public string Readme { get; private set; }

        public async Task OnGet(string name)
        {
            Chart = await getChartHandler.HandleAsync(new GetChart(name));
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            Readme = Markdown.ToHtml(await getChartReadmeHandler.HandleAsync(new GetChartReadme(Chart.Name, Chart.GetLastRelease()?.Version)), pipeline);
        }
    }
}