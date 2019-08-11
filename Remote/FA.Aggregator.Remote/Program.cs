using Akka.Actor;
using Akka.Routing;
using FA.Common.Actors;
using FA.Common;
using System.Threading.Tasks;

namespace FA.Aggregator.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();

            var faActorSystem = ActorSystem.Create(Constants.ActorSystemName, Utils.ParseConfig("app.hocon"));

            var agg = faActorSystem.ActorOf(Props.Create<AggregatorActor>().WithRouter(FromConfig.Instance), "aggregator");

            var supervisor = faActorSystem.ActorOf(Props.Create<ScanerSupervisorActorWithRouters>(agg), "scaners");

            Utils.CLI(supervisor);

            faActorSystem.WhenTerminated.Wait();
            
        }
    }
}
