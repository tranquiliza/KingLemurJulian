using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class RollCommand : CommandExecutorBase
    {
        public override string CommandName => "Roll";

        private readonly IMediator mediator;
        private readonly Random rng;

        public RollCommand(IMediator mediator)
        {
            this.mediator = mediator;
            rng = new Random();
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var commandAsString = commandRequest.Argument;
            if (double.TryParse(commandAsString, out var number))
            {
                var roll = rng.Next(1, (int)number + 1);
                await SendMessage(roll).ConfigureAwait(false);
            }
            else
            {
                var chance = rng.NextDouble();
                if (chance > 0.75 && !string.IsNullOrWhiteSpace(commandAsString))
                {
                    await mediator.Send(new CommandResponseRequest(commandRequest, $"Haha, you think I will accept your gibberish?! You can be a {commandAsString}")).ConfigureAwait(false);
                    return;
                }

                var roll = rng.Next(1, 21);
                await SendMessage(roll).ConfigureAwait(false);
            }

            async Task SendMessage(int roll)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, $"You rolled: {roll} with the D{(number == 0 ? 20 : number)}")).ConfigureAwait(false);
            }
        }
    }
}
