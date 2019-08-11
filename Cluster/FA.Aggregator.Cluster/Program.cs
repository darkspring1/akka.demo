using Akka.Actor;
using Akka.Routing;
using FA.Common;
using FA.Common.Actors;
using System.Threading.Tasks;

namespace FA.Aggregator.Cluster
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Delay(5000);

            var faActorSystem = ActorSystem.Create(Constants.ActorSystemName, Utils.ParseConfig("app.hocon"));

            var agg = faActorSystem.ActorOf(Props.Create<AggregatorActor>().WithRouter(FromConfig.Instance), "aggregator");

            faActorSystem.ActorOf(Props.Create<ScanerSupervisorActorWithRouters>(agg), "scaners");


            faActorSystem.WhenTerminated.Wait();
        }
    }
}
