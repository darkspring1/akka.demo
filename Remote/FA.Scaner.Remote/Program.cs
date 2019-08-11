using Akka.Actor;
using FA.Utils;

namespace FA.Scaner.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("AggregatorSystem", HoconLoader.ParseConfig("fa.scaner.remote.hocon"));
            actorSystem.WhenTerminated.Wait();
        }
    }
}
