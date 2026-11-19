using System;

namespace CoffeeShopApp
{
    // CoffeeShop now only coordinates: it asks the Order for its total,
    // prints the summary and hands the amount to the payment processor.
    public class CoffeeShop
    {
        private readonly IPaymentProcessor paymentProcessor;

        public Menu Menu { get; } = new Menu();

        public CoffeeShop() : this(new ConsolePaymentProcessor())
        {
        }

        public CoffeeShop(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        public void ProcessOrder(Order order)
        {
            double total = order.CalculateTotal(Menu);
            PrintOrderSummary(order.CustomerName, total);
            paymentProcessor.ProcessPayment(total);
        }

        // Old signature kept so existing callers still work unchanged.
        public void ProcessOrder(string customerName, bool wantsCoffee, bool wantsSandwich)
        {
            ProcessOrder(new Order(customerName, wantsCoffee, wantsSandwich));
        }

        public void UpdateMenu(double coffeePrice, double sandwichPrice)
        {
            Menu.UpdatePrices(coffeePrice, sandwichPrice);
        }

        private static void PrintOrderSummary(string customerName, double total)
        {
            Console.WriteLine("Order processed for " + customerName);
            Console.WriteLine("Total amount to pay: " + total);
        }
    }
}
