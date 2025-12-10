namespace ConsoleApp.Models
{
    internal class Pizza
    {
        public Pizza()
        {

        }

        public Pizza(bool hasCheese, bool hasPepperoni, bool hasMushrooms, bool hasOlives, bool hasPineapple, bool hasExtraCheese, bool hasBacon, bool hasSpinach, bool hasGarlic)
        {
            HasCheese = hasCheese;
            HasPepperoni = hasPepperoni;
            HasMushrooms = hasMushrooms;
            HasOlives = hasOlives;
            HasPineapple = hasPineapple;
            HasExtraCheese = hasExtraCheese;
            HasBacon = hasBacon;
            HasSpinach = hasSpinach;
            HasGarlic = hasGarlic;
        }

        public Pizza(bool hasCheese, bool hasPepperoni, bool hasMushrooms)
        {
            HasCheese = hasCheese;
            HasPepperoni = hasPepperoni;
            HasMushrooms = hasMushrooms;
        }

        public Pizza(bool hasCheese, bool hasPepperoni)
        {
            HasCheese = hasCheese;
            HasPepperoni = hasPepperoni;
        }

        public bool HasCheese { get; set; }
        public bool HasPepperoni { get; set; }
        public bool HasMushrooms { get; set; }
        public bool HasOlives { get; set; }
        public bool HasPineapple { get; set; }
        public bool HasExtraCheese { get; set; }
        public bool HasBacon { get; set; }
        public bool HasSpinach { get; set; }
        public bool HasGarlic { get; set; }
    }
}
