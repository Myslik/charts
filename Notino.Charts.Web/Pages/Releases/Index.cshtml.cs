using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Releases
{
    public class IndexModel : PageModel
    {
        private readonly IGetReleasesHandler getReleasesHandler;

        public IndexModel(
            IGetReleasesHandler getReleasesHandler)
        {
            this.getReleasesHandler = getReleasesHandler ?? throw new ArgumentNullException(nameof(getReleasesHandler));
        }

        public IEnumerable<HelmRelease> Releases { get; private set; }

        public async Task OnGet()
        {
            Releases = await getReleasesHandler.HandleAsync(new GetReleases());
        }
    }
}