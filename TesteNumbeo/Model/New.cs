namespace TesteNumbeo.Model
{
    public class New
    {
        public string Name { get; set; }
        public double _1h { get; set; }
        public double _24h { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Volume { get; set; }
        public string Added { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is New q && q.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
