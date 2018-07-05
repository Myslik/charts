using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGetChartsHandler getChartsHandler;

        public IndexModel(
            IGetChartsHandler getChartsHandler)
        {
            this.getChartsHandler = getChartsHandler ?? throw new ArgumentNullException(nameof(getChartsHandler));
        }

        public IEnumerable<Chart> Charts { get; private set; }

        public async Task OnGet()
        {
            Charts = await getChartsHandler.HandleAsync(new GetCharts());
        }
    }
}
