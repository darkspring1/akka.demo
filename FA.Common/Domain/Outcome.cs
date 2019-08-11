namespace FA.Common.Domain
{

    public class Outcome
    {
        public Outcome(Side side, decimal value)
        {
            Side = side;
            Value = value;
        }

        public Side Side { get; }
        public decimal Value { get; set; }
    }
}
