﻿using KingLemurJulian.Core.Events;
using MediatR;
using System.Globalization;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class PingCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        private readonly IDateTimeProvider dateTimeProvider;

        public PingCommand(IMediator mediator, IDateTimeProvider dateTimeProvider)
        {
            this.mediator = mediator;
            this.dateTimeProvider = dateTimeProvider;
        }

        public override string CommandName => "Ping";

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (!commandRequest.ChatMessage.IsBroadcaster)
                return false;

            return base.CanExecute(commandRequest);
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var responseText = $"PONG {commandRequest.ChatMessage.DisplayName}, from channel {commandRequest.ChatMessage.Channel} at {dateTimeProvider.Now.ToString("dd/MM/yyyy HH:mm:ss.ff", CultureInfo.InvariantCulture)} UTC";

            var response = new ChatResponseRequest(commandRequest, responseText);

            await mediator.Send(response).ConfigureAwait(false);
        }
    }
}
