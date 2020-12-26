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
    public class CommandRequestHandler : IRequestHandler<CommandRequest, Unit>
    {
        private readonly IList<ICommandExecutor> commandExecutors;
        private readonly ILogger<CommandRequestHandler> logger;

        internal CommandRequestHandler(IList<ICommandExecutor> commandExecutors, ILogger<CommandRequestHandler> logger)
        {
            this.commandExecutors = commandExecutors;
            this.logger = logger;
        }

        public async Task<Unit> Handle(CommandRequest request, CancellationToken cancellationToken)
        {
            var executer = commandExecutors.SingleOrDefault(x => x.CanExecute(request));
            if (executer == null)
                logger.LogInformation("User {user} attempted to execute command: {commandName}", request.ChatMessage.DisplayName, request.CommandText);
            else
                await executer.Execute(request).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
