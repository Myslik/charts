using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Releases
{
    public class DetailsModel : PageModel
    {
        private readonly IGetReleasesHandler getReleasesHandler;
        private readonly IGetReleaseDetailsHandler getReleaseDetailsHandler;

        public DetailsModel(
            IGetReleasesHandler getReleasesHandler,
            IGetReleaseDetailsHandler getReleaseDetailsHandler)
        {
            this.getReleasesHandler = getReleasesHandler ?? throw new ArgumentNullException(nameof(getReleasesHandler));
            this.getReleaseDetailsHandler = getReleaseDetailsHandler ?? throw new ArgumentNullException(nameof(getReleaseDetailsHandler));
        }

        public HelmRelease Release { get; private set; }

        public string ReleaseDetails { get; private set; }

        public async Task<ActionResult> OnGet(string context, string name)
        {
            var releases = await getReleasesHandler.HandleAsync(new GetReleases());
            Release = releases.SingleOrDefault(r => r.Name == name && r.Cluster == context);
            if (Release == null)
            {
                return NotFound();
            }
            ReleaseDetails = await getReleaseDetailsHandler.HandleAsync(new GetReleaseDetails(name, context));
            return Page();
        }
    }
}