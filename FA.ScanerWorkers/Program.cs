using Akka.Actor;
using FA.Common;
using FA.Utils;

namespace FA.SeedNode
{
    class Program
    {
        static void Main(string[] args)
        {
            var faActorSystem = ActorSystem.Create(Constants.ActorSystemName, HoconLoader.ParseConfig("app.hocon"));
            faActorSystem.WhenTerminated.Wait();
        }
    }
}
