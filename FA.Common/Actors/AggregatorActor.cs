﻿using Akka.Actor;
using Akka.Event;
using FA.Aggregator;
using FA.Common.Messages;
using System.Collections.Generic;

namespace FA.Common.Actors
{

    public class AggregatorActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private readonly Dictionary<int, ICanTell> _betEventActors;

        private ICanTell GetEventActor(int eventId)
        {
            if (!_betEventActors.TryGetValue(eventId, out var canTell))
            {
                canTell = Context.ActorOf(Props.Create(() => new BetEventActor()), $"betevent_{eventId}");
                _betEventActors[eventId] = canTell;
            }
            return canTell;
        }

        public AggregatorActor()
        {
            _betEventActors = new Dictionary<int, ICanTell>();

            Receive<ExternalBetEventMessage>(msg =>
            {
                GetEventActor(msg.Id).Tell(msg, Self);
            });


            Receive<ExternalOddMessage>(msg =>
            {
                GetEventActor(msg.EventId).Tell(msg, Self);
            });

        }

        protected override void PreStart()
        {
            _log.Info($"{nameof(AggregatorActor)}.{nameof(PreRestart)}");
            base.PreStart();
        }
    }
}
