using Akka.Configuration;
using System.IO;

namespace FA.utils
{
    /// <summary>
    /// Used to load <see cref="WebCrawler.Shared.Config"/> objects from stand-alone HOCON files.
    /// </summary>
    public static class HoconLoader
    {
        public static Akka.Configuration.Config ParseConfig(string hoconPath)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(hoconPath));
        }
    }
}
