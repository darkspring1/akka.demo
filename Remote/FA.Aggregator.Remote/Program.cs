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

            var faActorSystem = ActorSystem.Create("AggregatorSystem", HoconLoader.ParseConfig("fa.aggregator.remote.hocon"));

            var agg = faActorSystem.ActorOf(Props.Create<AggregatorActor>().WithRouter(FromConfig.Instance), "aggregator");

            faActorSystem.ActorOf(Props.Create<ScanerSupervisorActor>(agg), "scaners");
           
            

            var scaners = faActorSystem.ActorSelection("/user/scaners/*");

            faActorSystem
           .Scheduler
           .ScheduleTellRepeatedly(TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(5),
                scaners, new ScanCommand(), ActorRefs.NoSender);

            string cmd = null;
            while (cmd != "exit")
            {
                cmd = Console.ReadLine();
                if (cmd == "exc")
                {
                    scaners.Tell(new ExceptionCommand());
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
