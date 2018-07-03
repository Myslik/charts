using Notino.Charts.Domain;
using System.Collections.Generic;

namespace Notino.Charts.Queries
{
    public interface IGetChartsHandler : IQueryHandler<GetCharts, IEnumerable<Chart>>
    {
    }
}
