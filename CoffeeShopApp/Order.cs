namespace CoffeeShopApp
{
    // Parameter object: groups what the customer ordered instead of
    // passing (string, bool, bool) around.
    public class Order
    {
        public string CustomerName { get; }
        public bool WantsCoffee { get; }
        public bool WantsSandwich { get; }

        public Order(string customerName, bool wantsCoffee, bool wantsSandwich)
        {
            CustomerName = customerName;
            WantsCoffee = wantsCoffee;
            WantsSandwich = wantsSandwich;
        }

        // Extracted + moved from ProcessOrder: the order knows its own total.
        public double CalculateTotal(Menu menu)
        {
            double total = 0;
            if (WantsCoffee)
            {
                total += menu.CoffeePrice;
            }
            if (WantsSandwich)
            {
                total += menu.SandwichPrice;
            }
            return total;
        }
    }
}
