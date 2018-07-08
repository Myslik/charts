using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages.Contexts
{
    public class IndexModel : PageModel
    {
        private readonly IGetClustersHandler getClustersHandler;

        public IndexModel(
            IGetClustersHandler getClustersHandler)
        {
            this.getClustersHandler = getClustersHandler ?? throw new ArgumentNullException(nameof(getClustersHandler));
        }

        public IEnumerable<KubernetesContext> Contexts { get; private set; }

        public async Task OnGet()
        {
            Contexts = await getClustersHandler.HandleAsync(new GetClusters());
        }
    }
}