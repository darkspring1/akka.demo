using Akka.Actor;
using FA.Common.Domain;
using FA.Common.Messages;
using System.Linq;

namespace FA.Aggregator
{
    class BetEventActor : ReceiveActor
    {

        private AggregatedBetEvent _state;

        private Outcome[] ConvertOutcomes(ExternalOutcome[] outcomes)
        {
            return outcomes.Select(x => new Outcome(x.Side, x.Value)).ToArray();
        }

        public BetEventActor()
        {
            Receive<ExternalBetEventMessage>(msg =>
            {
                if (_state == null)
                {
                    _state = new AggregatedBetEvent();
                }

                _state.Id = msg.Id;
                _state.StartTime = msg.StartTime;
            });


            Receive<ExternalOddMessage>(msg =>
            {
                if (_state != null)
                {
                    _state.AddExternalOdd(msg.Provider, msg.MarketKind, ConvertOutcomes(msg.Outcomes));
                }
            });
        }
    }
}
