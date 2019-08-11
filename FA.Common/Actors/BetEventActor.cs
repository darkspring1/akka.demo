using Akka.Actor;
using Akka.Event;
using FA.Common.Domain;
using FA.Common.Messages;
using System.Linq;

namespace FA.Aggregator
{
    class BetEventActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private AggregatedBetEvent _state;

        private string Name => Self.Path.Name;

        private Outcome[] ConvertOutcomes(ExternalOutcome[] outcomes)
        {
            return outcomes.Select(x => new Outcome(x.Side, x.Value)).ToArray();
        }

        private void WaitingNewEvent()
        {
            Receive<ExternalBetEventMessage>(msg =>
            {
                _log.Info($"{Name} recive {nameof(ExternalBetEventMessage)}");
                _state = new AggregatedBetEvent();
                _state.Id = msg.Id;
                _state.StartTime = msg.StartTime;
                Become(ReadyToWork);
            });


            Receive<ExternalOddMessage>(msg =>
            {
                _log.Info($"{Name} recive {nameof(ExternalOddMessage)}");
                _log.Info("I'm waiting event.");
            });
        }

        private void ReadyToWork()
        {
            Receive<ExternalBetEventMessage>(msg =>
            {
                _log.Info($"{Name} recive {nameof(ExternalBetEventMessage)}");
                _state.StartTime = msg.StartTime;
            });


            Receive<ExternalOddMessage>(msg =>
            {
                _log.Info($"{Name} recive {nameof(ExternalOddMessage)}");
                _state.AddExternalOdd(msg.Provider, msg.MarketKind, ConvertOutcomes(msg.Outcomes));
            });
        }

        public BetEventActor()
        {
            WaitingNewEvent();
        }

        protected override void PreStart()
        {
            _log.Info($"{nameof(BetEventActor)}.{nameof(PreRestart)}");
            base.PreStart();
        }
    }
}
