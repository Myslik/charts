using System;
using System.Collections.ObjectModel;

namespace Notino.Charts.Domain
{
    public class HelmDeployment
    {
        public HelmDeployment(HelmRelease release, SemVer.Version chartVersion, ReadOnlyDictionary<string, string> values)
        {
            Release = release ?? throw new ArgumentNullException(nameof(release));
            ChartVersion = chartVersion ?? throw new ArgumentNullException(nameof(chartVersion));
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public HelmRelease Release { get; }
        public SemVer.Version ChartVersion { get; }
        public ReadOnlyDictionary<string, string> Values { get; }
    }
}
