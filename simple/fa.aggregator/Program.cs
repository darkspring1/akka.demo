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
            var faActorSystem = ActorSystem.Create("FAActorSystem", HoconLoader.ParseConfig("fa.aggregator.hocon"));
            
            var asian = faActorSystem.ActorOf<ScanerActor>("scaner_asian");
            var greenFeed = faActorSystem.ActorOf<ScanerActor>("scaner_greenFeed");
            var aggregator = faActorSystem.ActorOf<AggregatorActor>("aggregator");
           
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
