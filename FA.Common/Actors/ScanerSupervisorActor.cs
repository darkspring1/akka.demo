using Akka.Actor;
using Akka.Event;

namespace FA.Common.Actors
{
    public class ScanerSupervisorActor : UntypedActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        public ScanerSupervisorActor(IActorRef aggregator)
        {
            var asian = Context.ActorOf(Props.Create<ScanerActor>(aggregator), "asian");
            var greenFeed = Context.ActorOf(Props.Create<ScanerActor>(aggregator), "greenFeed");
        }

        protected override void OnReceive(object message)
        {
           
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                _log.Info("Scaner throw the exception");
                return Directive.Resume;
            });
        }
    }
}
