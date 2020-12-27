using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class QuoteCommand : CommandExecutorBase
    {
        public override string CommandName => "quote";

        private readonly IMediator mediator;
        private readonly IQuoteRepository quoteRepository;
        private readonly Random rng;

        public QuoteCommand(IMediator mediator, IQuoteRepository quoteRepository)
        {
            this.mediator = mediator;
            this.quoteRepository = quoteRepository;
            rng = new Random();
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            if (commandRequest.Arguments.Count >= 1)
            {
                var argumentZero = commandRequest.Arguments[0];
                if (!int.TryParse(argumentZero, out var id))
                    return;

                var quote = await quoteRepository.GetQuoteAsync(id).ConfigureAwait(false);
                if (quote == null)
                    return;

                await mediator.Send(new ChatResponseRequest(commandRequest, quote.GetFormattedQuote())).ConfigureAwait(false);
                return;
            }

            var allQuotes = await quoteRepository.GetQuotesAsync().ConfigureAwait(false);
            var quoteToSendIndex = rng.Next(0, allQuotes.Count);

            await mediator.Send(new ChatResponseRequest(commandRequest, allQuotes[quoteToSendIndex].GetFormattedQuote())).ConfigureAwait(false);
        }
    }
}
