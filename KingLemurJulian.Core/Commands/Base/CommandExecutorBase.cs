using KingLemurJulian.Core.Events;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public abstract class CommandExecutorBase : ICommandExecutor
    {
        public virtual string CommandName { get; }

        public virtual bool CanExecute(CommandEvent commandEvent)
           => string.Equals(commandEvent.CommandText, CommandName, StringComparison.OrdinalIgnoreCase);

        public abstract Task Execute(CommandEvent command);
    }
}
