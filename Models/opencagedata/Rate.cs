namespace rubiera.opencagedata
{
    public class Rate
    {
        int limit, remaining;
        long reset;

        public Rate()
        {
        }

        public int Limit
        {
            get => limit;
            set => limit = value;
        }

        public int Remaining
        {
            get => remaining;
            set => remaining = value;
        }

        public long Reset
        {
            get => reset;
            set => reset = value;
        }

        public override string ToString()
        {
            return limit + " " + remaining + " " + reset;
        }
    }
}