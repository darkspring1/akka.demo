using Akka.Actor;
using Akka.Routing;
using FA.Common.Actors;
using FA.Common.Messages;
using FA.Utils;
using System;
using System.Threading.Tasks;

namespace FA.Aggregator.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();

            var faActorSystem = ActorSystem.Create("AggregatorSystem", HoconLoader.ParseConfig("app.hocon"));

            var agg = faActorSystem.ActorOf(Props.Create<AggregatorActor>().WithRouter(FromConfig.Instance), "aggregator");

            var supervisor = faActorSystem.ActorOf(Props.Create<ScanerSupervisorActor>(agg), "scaners");
           
            

            string cmd = null;
            while (cmd != "exit")
            {
                cmd = Console.ReadLine();
                if (cmd == "exc")
                {
                    supervisor.Tell(new ExceptionCommand());
                }
                else
                {
                    Console.WriteLine($"Unknown command '{cmd}'");
                }
            }

            faActorSystem.WhenTerminated.Wait();
            
        }
    }
}
