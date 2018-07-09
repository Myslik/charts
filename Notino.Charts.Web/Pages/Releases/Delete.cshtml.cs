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
    public class DeleteModel : PageModel
    {
        private readonly IGetReleasesHandler getReleasesHandler;
        private readonly IDeleteReleaseHandler deleteReleaseHandler;

        public DeleteModel(
            IGetReleasesHandler getReleasesHandler,
            IDeleteReleaseHandler deleteReleaseHandler)
        {
            this.getReleasesHandler = getReleasesHandler ?? throw new ArgumentNullException(nameof(getReleasesHandler));
            this.deleteReleaseHandler = deleteReleaseHandler ?? throw new ArgumentNullException(nameof(deleteReleaseHandler));
        }

        public HelmRelease Release { get; private set; }

        public async Task<ActionResult> OnGet(string context, string name)
        {
            var releases = await getReleasesHandler.HandleAsync(new GetReleases());
            Release = releases.SingleOrDefault(r => r.Name == name && r.Cluster == context);
            if (Release == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnPost(string context, string name)
        {
            await deleteReleaseHandler.HandleAsync(new DeleteRelease(name, context));
            return RedirectToPage("/Releases/Index");
        }
    }
}