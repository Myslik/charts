using System;

namespace Notino.Charts.Domain
{
    public class ChartRelease
    {
        public ChartRelease(
            Chart chart, 
            SemVer.Version version,
            string description = null,
            string home = null,
            string icon = null)
        {
            Chart = chart ?? throw new ArgumentNullException(nameof(chart));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Description = description;
            Home = home;
            Icon = icon;
        }

        public Chart Chart { get; }
        public SemVer.Version Version { get; }
        public string Description { get; }
        public string Home { get; }
        public string Icon { get; }
    }
}
