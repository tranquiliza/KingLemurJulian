using KingLemurJulian.Core.Commands;
using KingLemurJulian.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class CommandEventHandler : INotificationHandler<CommandEvent>
    {
        private readonly IList<ICommandExecutor> commandExecutors;
        private readonly ILogger<CommandEventHandler> logger;

        public CommandEventHandler(IList<ICommandExecutor> commandExecutors, ILogger<CommandEventHandler> logger)
        {
            this.commandExecutors = commandExecutors;
            this.logger = logger;
        }

        public async Task Handle(CommandEvent commandEvent, CancellationToken cancellationToken)
        {
            var executer = commandExecutors.SingleOrDefault(x => x.CanExecute(commandEvent));
            if (executer == null)
                logger.LogInformation("User {user} attempted to execute command: {commandName}", commandEvent.ChatMessage.DisplayName, commandEvent.CommandText);
            else
                await executer.Execute(commandEvent).ConfigureAwait(false);
        }
    }
}
