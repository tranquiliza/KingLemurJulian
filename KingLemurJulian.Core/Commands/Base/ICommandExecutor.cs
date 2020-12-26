using KingLemurJulian.Core.Events;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public interface ICommandExecutor
    {
        string CommandName { get; }
        bool CanExecute(CommandEvent commandEvent);
        Task Execute(CommandEvent command);
    }
}
