using CsvHelper.Configuration;
using System;

namespace Notino.Charts.Helm
{
    public class Release
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public DateTime Updated { get; set; }
        public string Status { get; set; }
        public string Chart { get; set; }
        public string Namespace { get; set; }
    }

    public class ReleaseMap : ClassMap<Release>
    {
        public ReleaseMap()
        {
            Map(m => m.Name).Name("NAME");
            Map(m => m.Revision).Name("REVISION");
            Map(m => m.Updated).Name("UPDATED");
            Map(m => m.Status).Name("STATUS");
            Map(m => m.Chart).Name("CHART");
            Map(m => m.Namespace).Name("NAMESPACE");
        }
    }
}
