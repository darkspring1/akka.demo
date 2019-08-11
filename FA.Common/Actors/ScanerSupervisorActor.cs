using Akka.Actor;

namespace FA.Common.Actors
{
    public class ScanerSupervisorActor : UntypedActor
    {
        public ScanerSupervisorActor()
        {
            var asian = Context.ActorOf<ScanerActor>("asian");
            var greenFeed = Context.ActorOf<ScanerActor>("greenFeed");
        }

        protected override void OnReceive(object message)
        {
           
        }
    }
}
