using System.Threading.Tasks;

namespace Notino.Charts
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

}
