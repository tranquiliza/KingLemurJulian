using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class EightBallResponseHandler : INotificationHandler<ChatMessageEvent>
    {
        private readonly IMediator mediator;
        private readonly Random rng;
        private readonly IEightBallResponseRepository eightBallResponseRepository;

        public EightBallResponseHandler(IMediator mediator, IEightBallResponseRepository eightBallResponseRepository)
        {
            this.mediator = mediator;
            rng = new Random();
            this.eightBallResponseRepository = eightBallResponseRepository;
        }

        public async Task Handle(ChatMessageEvent chatMessage, CancellationToken cancellationToken)
        {
            var messageWithoutAt = chatMessage.Message.Trim('@');

            var responseDelay = rng.Next(1000, 4500);

            var eightBallTrigger = botNames.Any(name => messageWithoutAt.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            if (eightBallTrigger && messageWithoutAt.Length > 19)
            {
                var ballResponses = await eightBallResponseRepository.GetBallResponses().ConfigureAwait(false);
                var roll = rng.Next(0, ballResponses.Count);
                var reply = ballResponses[roll];
                await Task.Delay(responseDelay).ConfigureAwait(false);
                await mediator.Send(new ChatResponseRequest(chatMessage, reply)).ConfigureAwait(false);
                return;
            }

            var helloMessageTrigger = greetings.Any(greeting => messageWithoutAt.StartsWith(greeting, StringComparison.OrdinalIgnoreCase));
            var helloMessageTriggerTwo = botNames.Any(name => messageWithoutAt.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            if (helloMessageTrigger && helloMessageTriggerTwo)
            {
                var roll = rng.Next(0, greetings.Count);
                var response = greetings[roll];
                await Task.Delay(responseDelay).ConfigureAwait(false);
                await mediator.Send(new ChatResponseRequest(chatMessage, response + $" {chatMessage.Username}")).ConfigureAwait(false);
                return;
            }

            var listeningResponseTrigger = botNames.Any(name => messageWithoutAt.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            if (listeningResponseTrigger)
            {
                const double minimumRollToReply = 0.60;
                var rollForReply = rng.NextDouble();
                if (rollForReply < minimumRollToReply)
                    return; // we dont want to reply every time someone mentions name.

                if (string.Equals(chatMessage.Username, "ChromaCarina", StringComparison.OrdinalIgnoreCase))
                    return;

                var iAmListeningResponses = await eightBallResponseRepository.GetIAmListeningResponses().ConfigureAwait(false);
                var roll = rng.Next(0, iAmListeningResponses.Count);
                var response = iAmListeningResponses[roll];

                await Task.Delay(responseDelay).ConfigureAwait(false);
                await mediator.Send(new ChatResponseRequest(chatMessage, response)).ConfigureAwait(false);
                return;
            }
        }

        private readonly List<string> botNames = new List<string>
        {
            "KingLemurJulian",
            "Lemur",
            "Julian",
            "KingLemur"
        };

        private readonly List<string> greetings = new List<string>
        {
            "Hi",
            "Hello",
            "Hey",
            "HeyGuys"
        };
    }
}
