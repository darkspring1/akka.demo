using Akka.Actor;
using FA.Common;

namespace FA.Scaner.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create(Constants.ActorSystemName, Utils.ParseConfig("app.hocon"));
            actorSystem.WhenTerminated.Wait();
        }
    }
}
