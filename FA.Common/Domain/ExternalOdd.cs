namespace FA.Common.Domain
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
