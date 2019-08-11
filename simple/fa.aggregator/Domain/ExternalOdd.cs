namespace FA.Aggregator.Domain
{
    class ExternalOdd
    {
        public ExternalOdd(Outcome[] outcomes)
        {
            Outcomes = outcomes;
        }

        public Outcome[] Outcomes { get; }
    }
}
