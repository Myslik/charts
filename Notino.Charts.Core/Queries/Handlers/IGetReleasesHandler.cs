using Notino.Charts.Domain;
using System.Collections.Generic;

namespace Notino.Charts.Queries.Handlers
{
    public interface IGetReleasesHandler : IQueryHandler<GetReleases, IEnumerable<HelmRelease>>
    {
    }
}
