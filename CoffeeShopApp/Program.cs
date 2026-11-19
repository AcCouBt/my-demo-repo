// ====================================================================
//  Software Construction - Lab #8 : Refactoring
//  Task 2: Refactored Coffee Shop system.
//          Same inputs, same output - only the structure has changed.
//
//  Student : Muhammad Aashir Siddiqui
// ====================================================================

using System;

namespace CoffeeShopApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            RunDemo();
        }

        // Same orders as the driver used for the original code.
        public static void RunDemo()
        {
            Console.WriteLine("===== Coffee Shop App - Version 2.0 =====");
            Console.WriteLine("Today's special: Coffee + Sandwich combo!");
            Console.WriteLine();
            CoffeeShop shop = new CoffeeShop();
            shop.UpdateMenu(350, 450);

            shop.ProcessOrder(new Order("Ali", true, true));
            Console.WriteLine();
            shop.ProcessOrder(new Order("Sara", true, false));
            Console.WriteLine();

            shop.UpdateMenu(400, 500);
            shop.ProcessOrder(new Order("Ahmed", false, true));
            Console.WriteLine();
            shop.ProcessOrder("Bilal", false, false);   // old-style call still works
        }
    }
}
