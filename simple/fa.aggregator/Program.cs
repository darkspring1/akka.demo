using Akka.Actor;
using FA.Common.Actors;
using FA.Common.Messages;
using FA.utils;
using System;

namespace FA.Aggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            var faActorSystem = ActorSystem.Create("faActorSystem", HoconLoader.ParseConfig("fa.aggregator.hocon"));

            var aggregator = faActorSystem.ActorOf<AggregatorActor>("aggregator");
            var asian = faActorSystem.ActorOf(Props.Create<ScanerActor>(aggregator), "scaner_asian");
            var greenFeed = faActorSystem.ActorOf(Props.Create<ScanerActor>(aggregator), "scaner_greenFeed");
            
           
            var scaners = faActorSystem.ActorSelection("/user/scaner*");

            faActorSystem
           .Scheduler
           .ScheduleTellRepeatedly(TimeSpan.FromSeconds(0),
                TimeSpan.FromSeconds(5),
                scaners, new ScanCommand(), ActorRefs.NoSender);


            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
    }
}
