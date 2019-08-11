using System.Collections.Generic;

namespace FA.Aggregator.Domain
{
    class AggregatedOdd
    {
        private readonly Dictionary<Provider, ExternalOdd> _externalOdds = new Dictionary<Provider, ExternalOdd>();

        public AggregatedOdd(MarketKind marketKind)
        {
            MarketKind = marketKind;
           
        }

        public MarketKind MarketKind { get; }

        public void AddOrUpdateOdd(Provider provider, ExternalOdd odd)
        {
            _externalOdds[provider] = odd;
        }
    }
}
