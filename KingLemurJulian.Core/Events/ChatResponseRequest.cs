using MediatR;

namespace KingLemurJulian.Core.Events
{
    public class ChatResponseRequest : IRequest
    {
        public CommandRequest commandRequest { get; }
        public string Channel { get; }
        public string Response { get; }

        public ChatResponseRequest(CommandRequest commandRequest, string response)
        {
            this.commandRequest = commandRequest;
            Channel = commandRequest.ChatMessage.Channel;
            Response = response;
        }
    }
}
