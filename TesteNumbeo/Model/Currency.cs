using System.ComponentModel.DataAnnotations;

namespace TesteNumbeo.Model
{
    public class CurrencyModel
    {
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public HashSet<CurrencyValue> Currencies { get; set; } = new();
    }

    public class CurrencyValue
    {
        public CurrencyValue(string currency, decimal value)
        {
            this.Currency = currency;
            this.Value = value;
        }

        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is CurrencyValue q && q.Currency == Currency;
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode();
        }
    }
}