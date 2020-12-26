using MediatR;

namespace KingLemurJulian.Core.Events
{
    public class ChatResponseRequest : INotification
    {
        public CommandEvent CommandEvent { get; }
        public string Channel { get; }
        public string Response { get; }

        public ChatResponseRequest(CommandEvent command, string response)
        {
            CommandEvent = command;
            Channel = command.ChatMessage.Channel;
            Response = response;
        }
    }
}
