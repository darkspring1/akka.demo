using System;
using System.Collections.Generic;

namespace FA.Aggregator.Domain
{
    class AggregatedBetEvent
    {
        private readonly Dictionary<MarketKind, AggregatedOdd> _odds = new Dictionary<MarketKind, AggregatedOdd>();

        public DateTime StartTime { get; set; }
        public int Id { get; set; }

        public void AddExternalOdd(Provider provider, MarketKind marketKind, Outcome[] outcomes)
        {
            if (!_odds.TryGetValue(marketKind, out var aggOdd))
            {
                aggOdd = new AggregatedOdd(marketKind);
                _odds.Add(marketKind, aggOdd);
            }

            aggOdd.AddOrUpdateOdd(provider, new ExternalOdd(outcomes));
        }
    }
}
