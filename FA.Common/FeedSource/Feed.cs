using System;
using System.IO;
using FA.Common.Domain;
using Newtonsoft.Json;

namespace FA.Common.FeedSource
{
    public class BetEventDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class OddDto
    {
        public int EventId { get; set; }
        public MarketKind MarketKind { get; set; }
        public OutcomeDto[] Outcomes { get; set; }
    }

    public class OutcomeDto
    {
        public Side Side { get; set; }
        public decimal Value { get; set; }
    }

    static class Feed
    {
        public static BetEventDto[] BetEvents { get; }
        public static OddDto[] Odds { get; }

        private static T[] DeserializeObject<T>(string path)
        {
            var text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T[]>(text);
        }

        static Feed()
        {
            BetEvents = DeserializeObject<BetEventDto>("FeedSource/betEvents.json");
            Odds = DeserializeObject<OddDto>("FeedSource/odds.json");
        }
    }
}
