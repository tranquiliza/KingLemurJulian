using System;
using System.Collections.Generic;
using System.Text;

namespace KingLemurJulian.Core.Models
{
    public class Quote
    {
        public int Id { get; private set; }
        public string Channel { get; private set; }
        public string QuoteText { get; private set; }
        public DateTime CreationTime { get; private set; }
        public bool Deleted { get; private set; }

        [Obsolete("Only for serialization", true)]
        public Quote() { }

        public Quote(string channel, string quoteText, DateTime creationTime)
        {
            Channel = channel;
            QuoteText = quoteText;
            CreationTime = creationTime;
            Deleted = false;
        }

        private Quote(int id, string channel, string quoteText, DateTime creationTime, bool deleted)
        {
            Id = id;
            Channel = channel;
            QuoteText = quoteText;
            CreationTime = creationTime;
            Deleted = deleted;
        }

        public static Quote CreateFromDatabase(int id, string channel, string quoteText, DateTime creationTime, bool deleted)
            => new Quote(id, channel, quoteText, creationTime, deleted);

        public string GetFormattedQuote()
        {
            return $"Quote #{Id}: {QuoteText} {string.Format(new FancyDateTimeFormatProvider(), "{0}", CreationTime.Date)}";
        }
    }
}
