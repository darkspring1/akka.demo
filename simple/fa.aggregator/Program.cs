using Akka.Actor;
using FA.Common.Actors;
using FA.Common;
using System;

namespace FA.Aggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create(Constants.ActorSystemName, Utils.ParseConfig("app.hocon"));

            var aggregator = actorSystem.ActorOf(Props.Create<AggregatorActor>(), "aggregator");

            var supervisor = actorSystem.ActorOf(Props.Create<ScanerSupervisorActor>(aggregator), "scaners");

            Utils.CLI(supervisor);

            Console.WriteLine("Press any key.");

            Console.ReadKey();
        }
    }
}
