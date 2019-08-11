using Akka.Actor;
using Akka.Event;
using FA.Common.Domain;
using FA.Common.FeedSource;
using FA.Common.Messages;

namespace FA.Common.Actors
{


    public class ScanerActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Provider _provider;
        private readonly IActorRef _aggregator;

        private string Name => Self.Path.Name;

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

        public ScanerActor(IActorRef aggregator)
        {
            _provider = ParseProvider(Self.Path.Name);
            _aggregator = aggregator;

            Receive<ScanCommand>(msg =>
            {
                _log.Info($"{Name} Read information from feed...");
                foreach (var odd in Feed.Odds)
                {
                    _aggregator.Tell(Convert(odd), Self);
                }

                foreach (var betEvent in Feed.BetEvents)
                {
                    _aggregator.Tell(Convert(betEvent), Self);
                }
            });

            Receive<ExceptionCommand>(msg =>
            {
                _log.Info($"{Name} I have sent an exception command!");
                throw new System.Exception();
            });
            
        }

        protected override void PostStop()
        {
            _log.Info($"{Name} stoped.");
            base.PostStop();
        }

    }
}
