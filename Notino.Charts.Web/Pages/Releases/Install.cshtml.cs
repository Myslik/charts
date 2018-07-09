using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Commands;
using Notino.Charts.Commands.Handlers;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Releases
{
    public class InstallModel : PageModel
    {
        private readonly IGetChartHandler getChartHandler;
        private readonly IGetClustersHandler getClustersHandler;
        private readonly IGetChartValuesHandler getChartValuesHandler;
        private readonly IInstallChartHandler installChartHandler;

        public InstallModel(
            IGetChartHandler getChartHandler,
            IGetClustersHandler getClustersHandler,
            IGetChartValuesHandler getChartValuesHandler,
            IInstallChartHandler installChartHandler)
        {
            this.getChartHandler = getChartHandler ?? throw new ArgumentNullException(nameof(getChartHandler));
            this.getClustersHandler = getClustersHandler ?? throw new ArgumentNullException(nameof(getClustersHandler));
            this.getChartValuesHandler = getChartValuesHandler ?? throw new ArgumentNullException(nameof(getChartValuesHandler));
            this.installChartHandler = installChartHandler ?? throw new ArgumentNullException(nameof(installChartHandler));
        }

        public Chart Chart { get; private set; }
        public ChartRelease Release { get; private set; }
        public IEnumerable<KubernetesContext> Contexts { get; private set; }

        [BindProperty]
        public string KubernetesContext { get; set; }
        [BindProperty]
        public string ReleaseName { get; set; }
        [BindProperty]
        public string Values { get; set; }

        public async Task<ActionResult> OnGet(string chart, string version = null)
        {
            Chart = await getChartHandler.HandleAsync(new GetChart(chart));
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
            Contexts = await getClustersHandler.HandleAsync(new GetClusters());
            Values = await getChartValuesHandler.HandleAsync(new GetChartValues(chart, Release.Version.ToString()));
            return Page();
        }

        public async Task<ActionResult> OnPost(string chart, string version = null)
        {
            Chart = await getChartHandler.HandleAsync(new GetChart(chart));
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
                return BadRequest();
            }
            await installChartHandler.HandleAsync(new InstallChart(chart, Release.Version.ToString(), ReleaseName, KubernetesContext, Values));
            return RedirectToPage("/Releases/Index");
        }
    }
}