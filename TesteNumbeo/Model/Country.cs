using System.ComponentModel.DataAnnotations;
using TesteNumbeo.Core;

namespace TesteNumbeo.Model
{
    public class CountryModel
    {
        public HashSet<Country> Countries { get; set; } = new();
    }

    public class Country
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Uri Link { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public bool CanWork { get; set; } = false;

        [Required]
        public decimal MinimalWage { get; set; } = 0;

        [Required]
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;

        public HashSet<Expense> Expenses { get; set; } = new();

        public HashSet<City> Cities { get; set; } = new();

        public decimal Total(PriceType type, HashSet<CurrencyValue> currencies) => Expenses.Sum(s => s.GetDolar(type, currencies, Currency, s.Type == ExpenseType.Meal ? 30 : 1));

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is Country q && q.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class City
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Uri Link { get; set; }

        [Required]
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;

        public HashSet<Expense> Expenses { get; set; } = new();

        public decimal Total(PriceType type, HashSet<CurrencyValue> currencies, string Currency) => Expenses.Sum(s => s.GetDolar(type, currencies, Currency, s.Type == ExpenseType.Meal ? 30 : 1));

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is City q && q.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Expense
    {
        public ExpenseType Type { get; set; }
        public decimal MinPrice { get; set; }
        public decimal Price { get; set; }
        public decimal MaxPrice { get; set; }

        public decimal GetDolar(PriceType type, HashSet<CurrencyValue> currencies, string currency, int plus = 1)
        {
            var curr = currencies.First(f => f.Currency == currency);

            return type switch
            {
                PriceType.Minimum => MinPrice / curr.Value * plus,
                PriceType.Average => Price / curr.Value * plus,
                PriceType.Maximum => MaxPrice / curr.Value * plus,
                _ => 0,
            };
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is Expense q && q.Type == Type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }

    public enum PriceType
    {
        [Custom(Name = "Minimum Price")]
        Minimum = 1,

        [Custom(Name = "Average Price")]
        Average = 2,

        [Custom(Name = "Maximum Price")]
        Maximum = 3
    }

    public enum ExpenseType
    {
        [Custom(Name = "Apartment", Description = "Apartment (1 bedroom) in City Centre")]
        RentCentre = 0,

        [Custom(Name = "Meal", Description = "Meal, Inexpensive Restaurant")]
        Meal = 1,

        [Custom(Name = "Bills", Description = "Basic (Electricity, Heating, Cooling, Water, Garbage) for 915 sq ft Apartment")]
        Basic = 2,

        [Custom(Name = "Internet", Description = "Internet (60 Mbps or More, Unlimited Data, Cable/ADSL)")]
        Internet = 3,

        [Custom(Name = "Market", Description = "Recommended Minimum Amount of Money for food (2400 calories, Western food types)")]
        Market = 4
    }

    public static class Convertions
    {
        public const double Gallon = 3.785411784;
        public const double Pound = 0.453592;
    }

    public enum MarketExpenseType
    {
        [MarketCustom(Name = "Milk", Description = "Milk (regular), (1 gallon)", Proporcion = 0.25, Convert = Convertions.Gallon)]
        Milk = 0,

        [MarketCustom(Name = "White Bread", Description = "Loaf of Fresh White Bread (1 lb)", Proporcion = 0.25, Convert = Convertions.Pound * 2)]
        WhiteBread = 1,

        [MarketCustom(Name = "Rice", Description = "Rice (white), (1 lb)", Proporcion = 0.1, Convert = Convertions.Pound)]
        Rice = 2,

        [MarketCustom(Name = "Eggs", Description = "Eggs (regular) (12)", Proporcion = 0.2)]
        Eggs = 3,

        [MarketCustom(Name = "Cheese", Description = "Local Cheese (1 lb)", Proporcion = 0.1, Convert = Convertions.Pound)]
        Cheese = 4,

        [MarketCustom(Name = "Chicken", Description = "Chicken Fillets (1 lb)", Proporcion = 0.15, Convert = Convertions.Pound)]
        Chicken = 5,

        [MarketCustom(Name = "Beef", Description = "Beef Round (1 lb) (or Equivalent Back Leg Red Meat)", Proporcion = 0.15, Convert = Convertions.Pound)]
        Beef = 6,

        [MarketCustom(Name = "Apples", Description = "Apples (1 lb)", Proporcion = 0.3, Convert = Convertions.Pound)]
        Apples = 7,

        [MarketCustom(Name = "Banana", Description = "Banana (1 lb)", Proporcion = 0.25, Convert = Convertions.Pound)]
        Banana = 8,

        [MarketCustom(Name = "Oranges", Description = "Oranges (1 lb)", Proporcion = 0.3, Convert = Convertions.Pound)]
        Oranges = 9,

        [MarketCustom(Name = "Tomato", Description = "Tomato (1 lb)", Proporcion = 0.2, Convert = Convertions.Pound)]
        Tomato = 10,

        [MarketCustom(Name = "Potato", Description = "Potato (1 lb)", Proporcion = 0.2, Convert = Convertions.Pound)]
        Potato = 11,

        [MarketCustom(Name = "Onion", Description = "Onion (1 lb)", Proporcion = 0.1, Convert = Convertions.Pound)]
        Onion = 12,

        [MarketCustom(Name = "Lettuce", Description = "Lettuce (1 head)", Proporcion = 0.2)]
        Lettuce = 13,
    }
}