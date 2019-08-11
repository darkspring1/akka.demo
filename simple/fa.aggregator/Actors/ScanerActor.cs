using Akka.Actor;
using FA.Aggregator.Domain;
using FA.Aggregator.FeedSource;
using FA.Aggregator.Messages;

namespace FA.Aggregator.Actors
{
    class ScanerActor : ReceiveActor
    {
        private readonly Provider _provider;
        private readonly ICanTell _aggregator;

        private Provider ParseProvider(string name)
        {
            if (name.Contains("asian"))
            {
                return Provider.Asian;
            }
            return Provider.GreenFeed; 
        }

        private ExternalOutcome[] Convert(OutcomeDto[] dtos)
        {
            var result = new ExternalOutcome[dtos.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new ExternalOutcome(dtos[i].Side, dtos[i].Value);
            }
            return result;
        }

        private ExternalOddMessage Convert(OddDto odd)
        {
            return new ExternalOddMessage(_provider, odd.EventId, odd.MarketKind, Convert(odd.Outcomes));
        }

        private ExternalBetEventMessage Convert(BetEventDto dto)
        {
            return new ExternalBetEventMessage(_provider, dto.Id, dto.StartTime);
        }

        public ScanerActor()
        {
            _provider = ParseProvider(Self.Path.Name);
            _aggregator = Context.ActorSelection("/user/aggregator");
            Receive<ScanCommand>(msg =>
            {
                foreach (var odd in Feed.Odds)
                {
                    _aggregator.Tell(Convert(odd), Self);
                }

                foreach (var betEvent in Feed.BetEvents)
                {
                    _aggregator.Tell(Convert(betEvent), Self);
                }
            });
            
        }
    }
}
