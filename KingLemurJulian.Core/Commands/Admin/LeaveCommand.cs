using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class LeaveCommand : CommandExecutorBase
    {
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;
        private readonly IMediator mediator;

        public LeaveCommand(IChatClient chatClient, IChannelRepository channelRepository, IMediator mediator)
        {
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
            this.mediator = mediator;
        }

        public override string CommandName => "Leave";

        public override bool CanExecute(CommandEvent commandEvent)
        {
            if (!commandEvent.ChatMessage.IsBroadcaster || !string.Equals("tranquiliza", commandEvent.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return false;

            return base.CanExecute(commandEvent);
        }

        public override async Task Execute(CommandEvent command)
        {
            var channelName = command.Argument;
            if (string.IsNullOrEmpty(channelName))
                channelName = command.ChatMessage.Channel;

            await mediator.Publish(new ChatResponseRequest(command, "Leaving Channel. Goodbye!")).ConfigureAwait(false);

            chatClient.LeaveChannel(channelName);

            await channelRepository.DeleteChannel(channelName).ConfigureAwait(false);
        }
    }
}
