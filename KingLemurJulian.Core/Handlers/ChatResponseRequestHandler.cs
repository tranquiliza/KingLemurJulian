using KingLemurJulian.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class ChatResponseRequestHandler : INotificationHandler<ChatResponseRequest>
    {
        private readonly IChatMessageSender chatMessageSender;
        private readonly ILogger<ChatResponseRequestHandler> logger;

        public ChatResponseRequestHandler(IChatMessageSender chatMessageSender, ILogger<ChatResponseRequestHandler> logger)
        {
            this.chatMessageSender = chatMessageSender;
            this.logger = logger;
        }

        public Task Handle(ChatResponseRequest chatResponse, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Sent response from command {command}, to channel {channel}, requested by user {user}",
                chatResponse.CommandEvent.CommandText,
                chatResponse.Channel,
                chatResponse.CommandEvent.ChatMessage.DisplayName);

            chatMessageSender.SendMessage(chatResponse.CommandEvent.ChatMessage.Channel, chatResponse.Response);

            return Task.CompletedTask;
        }
    }
}
