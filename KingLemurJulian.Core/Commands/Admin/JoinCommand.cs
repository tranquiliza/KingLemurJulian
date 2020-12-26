using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class JoinCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;

        public override string CommandName => "Join";

        public JoinCommand(IMediator mediator, IChatClient chatClient, IChannelRepository channelRepository)
        {
            this.mediator = mediator;
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
        }

        public override bool CanExecute(CommandEvent commandEvent)
        {
            if (!string.Equals("tranquiliza", commandEvent.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return false;

            return base.CanExecute(commandEvent);
        }

        public override async Task Execute(CommandEvent command)
        {
            var channelToJoin = command.Argument;
            chatClient.JoinChannel(channelToJoin);

            await channelRepository.SaveChannel(channelToJoin).ConfigureAwait(false);

            await mediator.Publish(new ChatResponseRequest(command, "Joined channel " + command.Argument));
        }
    }
}
