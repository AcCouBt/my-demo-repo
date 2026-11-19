using System;

namespace CoffeeShopApp
{
    // Extracted interface: the contract for taking a payment.
    // CoffeeShop depends on this, not on one fixed way of paying.
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    // Moved from CoffeeShop.ProcessPayment - same output as before.
    public class ConsolePaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine("Processing payment of " + amount);
        }
    }
}
