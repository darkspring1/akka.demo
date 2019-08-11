using Akka.Actor;
using FA.Common;
using FA.Common;

namespace FA.SeedNode
{
    class Program
    {
        static void Main(string[] args)
        {
            var faActorSystem = ActorSystem.Create(Constants.ActorSystemName, Utils.ParseConfig("app.hocon"));
            faActorSystem.WhenTerminated.Wait();
        }
    }
}
