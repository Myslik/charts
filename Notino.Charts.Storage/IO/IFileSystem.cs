using System.IO;
using System.Threading.Tasks;

namespace Notino.Charts.IO
{
    public interface IFileSystem
    {
        string ChartDirectory { get; }
        Task SaveAsync(string name, Stream stream);
        Task<string> ReadIndexAsync();
    }
}
