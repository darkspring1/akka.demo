using FA.Common.Domain;
using System;

namespace FA.Common.Messages
{

    class ExternalBetEventMessage
    {
        public ExternalBetEventMessage(Provider provider, int id, DateTime startTime)
        {
            Provider = provider;
            Id = id;
            StartTime = startTime;
        }

        public Provider Provider { get; }
        public int Id { get; }
        public DateTime StartTime { get; }
    }
}
