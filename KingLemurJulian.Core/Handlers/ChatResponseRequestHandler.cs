using KingLemurJulian.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class ChatResponseRequestHandler : IRequestHandler<ChatResponseRequest, Unit>
    {
        private static class LogFormat
        {
            internal static void LogResponse(ILogger logger, ChatResponseRequest chatResponse)
            {
                logger.LogInformation(
                "Sent response from command {command}, to channel {channel}, requested by user {user}",
                chatResponse.CommandRequest.CommandText,
                chatResponse.Channel,
                chatResponse.CommandRequest.ChatMessage.DisplayName);
            }
        }

        private readonly IChatMessageSender chatMessageSender;
        private readonly ILogger<ChatResponseRequestHandler> logger;

        public ChatResponseRequestHandler(IChatMessageSender chatMessageSender, ILogger<ChatResponseRequestHandler> logger)
        {
            this.chatMessageSender = chatMessageSender;
            this.logger = logger;
        }

        public Task<Unit> Handle(ChatResponseRequest request, CancellationToken cancellationToken)
        {
            LogFormat.LogResponse(logger, request);

            chatMessageSender.SendMessage(request.CommandRequest.ChatMessage.Channel, request.Response);

            return Unit.Task;
        }
    }
}
