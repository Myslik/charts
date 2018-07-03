using System;
using System.IO;
using System.Threading.Tasks;

namespace Notino.Charts.IO
{
    public class FileSystem : IFileSystem
    {
        public string ChartDirectory { get; }

        public FileSystem(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("message", nameof(path));
            }

            ChartDirectory = path;
        }

        public async Task<string> ReadIndexAsync()
        {
            var index = Path.Combine(ChartDirectory, "index.yaml");
            using (var stream = File.Open(index, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task SaveAsync(string name, Stream stream)
        {
            var filename = Path.Combine(ChartDirectory, name);
            EnsureDirectoryExists(Path.GetDirectoryName(filename));
            using (var writer = File.OpenWrite(filename))
            {
                await stream.CopyToAsync(writer);
            }
        }

        private void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
