using System;

namespace CoffeeShopApp
{
    // Extracted class: the menu owns the prices.
    // Encapsulated: prices are read-only from outside and can only be
    // changed through UpdatePrices().
    public class Menu
    {
        public double CoffeePrice { get; private set; }
        public double SandwichPrice { get; private set; }

        // Lab 9: boundary testing showed a negative price was accepted,
        // which would make an order total negative. Prices must now be
        // 0 or more (0 = free item is allowed).
        public void UpdatePrices(double coffeePrice, double sandwichPrice)
        {
            if (coffeePrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coffeePrice),
                    "Price cannot be negative.");
            }
            if (sandwichPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sandwichPrice),
                    "Price cannot be negative.");
            }
            CoffeePrice = coffeePrice;
            SandwichPrice = sandwichPrice;
        }
    }
}
