using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Commands;
using Notino.Charts.Commands.Handlers;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Releases
{
    public class UpgradeModel : PageModel
    {
        private readonly IGetReleasesHandler getReleasesHandler;
        private readonly IGetChartHandler getChartHandler;
        private readonly IGetReleaseValuesHandler getReleaseValuesHandler;
        private readonly IUpgradeReleaseHandler upgradeReleaseHandler;

        public UpgradeModel(
            IGetReleasesHandler getReleasesHandler,
            IGetChartHandler getChartHandler,
            IGetReleaseValuesHandler getReleaseValuesHandler,
            IUpgradeReleaseHandler upgradeReleaseHandler)
        {
            this.getReleasesHandler = getReleasesHandler ?? throw new ArgumentNullException(nameof(getReleasesHandler));
            this.getChartHandler = getChartHandler ?? throw new ArgumentNullException(nameof(getChartHandler));
            this.getReleaseValuesHandler = getReleaseValuesHandler ?? throw new ArgumentNullException(nameof(getReleaseValuesHandler));
            this.upgradeReleaseHandler = upgradeReleaseHandler ?? throw new ArgumentNullException(nameof(upgradeReleaseHandler));
        }

        public Chart Chart { get; private set; }
        public HelmRelease Release { get; private set; }

        public async Task<ActionResult> OnGet(string context, string name)
        {
            var releases = await getReleasesHandler.HandleAsync(new GetReleases());
            Release = releases.SingleOrDefault(r => r.Name == name && r.Cluster == context);
            if (Release == null)
            {
                return NotFound();
            }
            Chart = await getChartHandler.HandleAsync(new GetChart(Release.ChartName));
            Values = await getReleaseValuesHandler.HandleAsync(new GetReleaseValues(Release.Name, Release.Cluster));
            Version = Release.ChartVersion;
            return Page();
        }

        [BindProperty]
        public string Values { get; set; }
        [BindProperty]
        public string Version { get; set; }

        public async Task<ActionResult> OnPost(string context, string name)
        {
            var releases = await getReleasesHandler.HandleAsync(new GetReleases());
            Release = releases.SingleOrDefault(r => r.Name == name && r.Cluster == context);
            if (Release == null)
            {
                return NotFound();
            }
            Chart = await getChartHandler.HandleAsync(new GetChart(Release.ChartName));
            await upgradeReleaseHandler.HandleAsync(new UpgradeRelease(name, Chart.Name, Version, Release.Cluster, Values));
            return RedirectToPage("/Releases/Index");
        }
    }
}