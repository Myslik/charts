using Notino.Charts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notino.Charts.Queries
{
    public interface IGetChartReleasesHandler : IQueryHandler<GetChartReleases, IEnumerable<ChartRelease>>
    {
    }
}
