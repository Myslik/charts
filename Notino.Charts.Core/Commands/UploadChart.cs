using System;
using System.IO;

namespace Notino.Charts.Commands
{
    public class UploadChart : ICommand
    {
        public UploadChart(string name, Stream stream)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public string Name { get; }
        public Stream Stream { get; }
    }
}
