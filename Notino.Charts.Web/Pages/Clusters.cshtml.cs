using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Charts.Domain;
using Notino.Charts.Queries;
using Notino.Charts.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Pages
{
    public class ClustersModel : PageModel
    {
        private readonly IGetClustersHandler getClustersHandler;

        public ClustersModel(
            IGetClustersHandler getClustersHandler)
        {
            this.getClustersHandler = getClustersHandler ?? throw new ArgumentNullException(nameof(getClustersHandler));
        }

        public IEnumerable<KubernetesCluster> Clusters { get; private set; }

        public async Task OnGet()
        {
            Clusters = await getClustersHandler.HandleAsync(new GetClusters());
        }
    }
}