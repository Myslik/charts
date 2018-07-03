using System;

namespace Notino.Charts.Domain
{
    public class Chart
    {
        public Chart(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public string Description { get; set; }

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
