using Notino.Charts.Domain;
using System.Collections.Generic;

namespace Notino.Charts.Queries.Handlers
{
    public interface IGetChartsHandler : IQueryHandler<GetCharts, IEnumerable<Chart>>
    {
    }
}
