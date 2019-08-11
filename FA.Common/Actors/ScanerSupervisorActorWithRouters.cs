using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using FA.Common.Messages;
using System;

namespace FA.Common.Actors
{
    public class ScanerSupervisorActorWithRouters : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        public ScanerSupervisorActorWithRouters(IActorRef aggregator)
        {
            var asian = Context.ActorOf(Props.Create<ScanerActor>(aggregator).WithRouter(FromConfig.Instance), "asian");
            var greenFeed = Context.ActorOf(Props.Create<ScanerActor>(aggregator).WithRouter(FromConfig.Instance), "greenFeed");

            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), asian, new ScanCommand(), Self);
            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), greenFeed, new ScanCommand(), Self);

            Receive<ExceptionCommand>(msg =>
            {
                asian.Tell(msg);
                greenFeed.Tell(msg);
            });
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
