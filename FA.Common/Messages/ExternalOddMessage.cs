using FA.Common.Domain;

namespace FA.Common.Messages
{
    public class ExternalOutcome
    {
        public ExternalOutcome(Side side, decimal value)
        {
            Side = side;
            Value = value;
        }

        public Side Side { get; }
        public decimal Value { get; set; }
    }

    class ExternalOddMessage
    {
        public ExternalOddMessage(Provider provider, int eventId, MarketKind marketKind, ExternalOutcome[] outcomes)
        {
            Provider = provider;
            EventId = eventId;
            MarketKind = marketKind;
            Outcomes = outcomes;
        }

        public Provider Provider { get; }
        public int EventId { get; }
        public MarketKind MarketKind { get; }
        public ExternalOutcome[] Outcomes { get; }
    }
}
