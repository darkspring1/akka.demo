using Akka.Actor;
using Akka.Configuration;
using FA.Common.Messages;
using System;
using System.IO;

namespace FA.Common
{
    /// <summary>
    /// Used to load <see cref="WebCrawler.Shared.Config"/> objects from stand-alone HOCON files.
    /// </summary>
    public static class Utils
    {
        public static Akka.Configuration.Config ParseConfig(string hoconPath)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(hoconPath));
        }

        public static void CLI(IActorRef supervisor)
        {
            string cmd = null;
            while (cmd != "exit")
            {
                Console.WriteLine("1 Send Exception to scaners.");
                cmd = Console.ReadLine();
                if (cmd == "1")
                {
                    supervisor.Tell(new ExceptionCommand());
                }
                else
                {
                    Console.WriteLine($"Unknown command '{cmd}'");
                }
            }
        }
    }
}
