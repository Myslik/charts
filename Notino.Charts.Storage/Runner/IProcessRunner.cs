using System.Threading.Tasks;

namespace Notino.Charts.Runner
{
    public interface IProcessRunner
    {
        Task<ProcessResult> RunProcessAsync(string fileName, string arguments);
    }
}