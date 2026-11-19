// ====================================================================
//  Software Construction - Lab #9 : Unit Testing
//  Task 3: Unit tests for the amount calculation of the refactored
//          Coffee Shop application (Lab 8, Task 2).
//          Unit under test: Order.CalculateTotal(Menu) and Menu prices.
//
//  Student : Muhammad Aashir Siddiqui
// ====================================================================

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeShopApp;

namespace CoffeeShopApp.UnitTests
{
    [TestClass]
    public class OrderAmountTests
    {
        // Allowed rounding difference when comparing double amounts.
        private const double Delta = 0.001;

        private Menu menu;

        // Fresh menu with the prices used in the Lab 8 demo.
        [TestInitialize]
        public void SetUp()
        {
            menu = new Menu();
            menu.UpdatePrices(350, 450);
        }

        // TC-C01: all 4 combinations of the two if-statements in
        // CalculateTotal -> full branch coverage (true/false for each).
        [DataTestMethod]
        [DataRow(true, true, 800.0)]     // coffee + sandwich
        [DataRow(true, false, 350.0)]    // coffee only
        [DataRow(false, true, 450.0)]    // sandwich only
        [DataRow(false, false, 0.0)]     // nothing ordered
        public void CalculateTotal_ItemCombination_ReturnsExpectedAmount(
            bool wantsCoffee, bool wantsSandwich, double expectedTotal)
        {
            // Arrange
            Order order = new Order("Ali", wantsCoffee, wantsSandwich);

            // Act
            double total = order.CalculateTotal(menu);

            // Assert
            Assert.AreEqual(expectedTotal, total, Delta);
        }

        // TC-C02: totals with different prices, incl. decimals and zero.
        [DataTestMethod]
        [DataRow(0.0, 0.0, 0.0)]            // lower boundary: free items
        [DataRow(0.0, 450.0, 450.0)]        // one free item
        [DataRow(99.99, 150.01, 250.0)]     // decimal prices
        [DataRow(1000.0, 2500.0, 3500.0)]   // large prices
        public void CalculateTotal_DifferentPrices_ReturnsSumOfPrices(
            double coffeePrice, double sandwichPrice, double expectedTotal)
        {
            // Arrange
            menu.UpdatePrices(coffeePrice, sandwichPrice);
            Order order = new Order("Sara", true, true);

            // Act
            double total = order.CalculateTotal(menu);

            // Assert
            Assert.AreEqual(expectedTotal, total, Delta);
        }

        // TC-C03: a price change affects the NEXT calculation.
        [TestMethod]
        public void CalculateTotal_AfterPriceUpdate_UsesNewPrices()
        {
            // Arrange
            Order order = new Order("Ahmed", true, true);
            double oldTotal = order.CalculateTotal(menu);

            // Act
            menu.UpdatePrices(400, 500);
            double newTotal = order.CalculateTotal(menu);

            // Assert
            Assert.AreEqual(800.0, oldTotal, Delta);
            Assert.AreEqual(900.0, newTotal, Delta);
        }

        // TC-C04: boundary/exception - negative prices are rejected.
        [DataTestMethod]
        [DataRow(-1.0, 450.0)]     // coffee price just below 0
        [DataRow(350.0, -0.01)]    // sandwich price just below 0
        [DataRow(-350.0, -450.0)]  // both negative
        public void UpdatePrices_NegativePrice_ThrowsArgumentOutOfRangeException(
            double coffeePrice, double sandwichPrice)
        {
            // Arrange: menu from SetUp (350 / 450)

            // Act + Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => menu.UpdatePrices(coffeePrice, sandwichPrice));

            // the old prices are kept
            Assert.AreEqual(350.0, menu.CoffeePrice, Delta);
            Assert.AreEqual(450.0, menu.SandwichPrice, Delta);
        }

        // TC-C05: boundary - a price of exactly 0 is accepted.
        [TestMethod]
        public void UpdatePrices_ZeroPrices_AreAccepted()
        {
            // Arrange
            double zero = 0.0;

            // Act
            menu.UpdatePrices(zero, zero);

            // Assert
            Assert.AreEqual(0.0, menu.CoffeePrice, Delta);
            Assert.AreEqual(0.0, menu.SandwichPrice, Delta);
        }
    }
}
