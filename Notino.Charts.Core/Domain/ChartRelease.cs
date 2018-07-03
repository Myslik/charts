using System;

namespace Notino.Charts.Domain
{
    public class ChartRelease
    {
        public ChartRelease(Chart chart, SemVer.Version version)
        {
            Chart = chart ?? throw new ArgumentNullException(nameof(chart));
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }

        public Chart Chart { get; }
        public SemVer.Version Version { get; }
    }
}
