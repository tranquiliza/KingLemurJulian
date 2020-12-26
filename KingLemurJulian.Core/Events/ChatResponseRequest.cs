using MediatR;

namespace KingLemurJulian.Core.Events
{
    public class ChatResponseRequest : IRequest
    {
        public CommandRequest CommandRequest { get; }
        public string Channel { get; }
        public string Response { get; }

        public ChatResponseRequest(CommandRequest commandRequest, string response)
        {
            CommandRequest = commandRequest;
            Channel = commandRequest.ChatMessage.Channel;
            Response = response;
        }
    }
}
