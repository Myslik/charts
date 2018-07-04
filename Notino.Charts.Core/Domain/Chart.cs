using System;
using System.Collections.Generic;
using System.Linq;

namespace Notino.Charts.Domain
{
    public class Chart
    {
        public Chart(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Releases = new List<ChartRelease>();
        }

        public string Name { get; }

        public string Description { get { return GetLastRelease()?.Description; } }

        public string Home { get { return GetLastRelease()?.Home; } }

        public string Icon { get { return GetLastRelease()?.Icon; } }

        public List<ChartRelease> Releases { get; }

        public ChartRelease GetLastRelease()
        {
            return Releases.OrderByDescending(r => r.Version).FirstOrDefault();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Chart ch = (Chart)obj;
            return ch.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"Chart [Name: {Name}]";
        }
    }
}
