using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class C2FCommand : CommandExecutorBase
    {
        public override string CommandName => "C2F";

        private readonly IMediator mediator;

        public C2FCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task Execute(CommandEvent command)
        {
            if (!double.TryParse(command.Arguments.FirstOrDefault(), out var number))
            {
                await mediator.Publish(new ChatResponseRequest(command, "Please give me a valid number"));
                return;
            }

            var result = (number * 9 / 5) + 32;
            await mediator.Publish(new ChatResponseRequest(command, $"{number}°C converts to {result.ToInvarientStringWith2Decimals()}°F")).ConfigureAwait(false);
        }
    }
}
