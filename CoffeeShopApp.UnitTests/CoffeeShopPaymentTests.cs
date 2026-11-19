// ====================================================================
//  Software Construction - Lab #9 : Unit Testing
//  Task 3: Checks that CoffeeShop passes the CALCULATED amount to the
//          payment step. A fake payment processor (test double) records
//          the amount instead of printing it, so the test can check it.
//
//  Student : Muhammad Aashir Siddiqui
// ====================================================================

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeShopApp;

namespace CoffeeShopApp.UnitTests
{
    // Test double: records every amount it is asked to charge.
    internal class FakePaymentProcessor : IPaymentProcessor
    {
        public List<double> Payments { get; } = new List<double>();

        public void ProcessPayment(double amount)
        {
            Payments.Add(amount);
        }
    }

    [TestClass]
    public class CoffeeShopPaymentTests
    {
        private const double Delta = 0.001;

        private FakePaymentProcessor fakePayment;
        private CoffeeShop shop;

        [TestInitialize]
        public void SetUp()
        {
            fakePayment = new FakePaymentProcessor();
            shop = new CoffeeShop(fakePayment);     // dependency injection
            shop.UpdateMenu(350, 450);
        }

        // TC-C06: the payment processor is charged exactly the order total.
        [TestMethod]
        public void ProcessOrder_CoffeeAndSandwich_ChargesTotalOnce()
        {
            // Arrange
            Order order = new Order("Ali", true, true);

            // Act
            shop.ProcessOrder(order);

            // Assert
            Assert.AreEqual(1, fakePayment.Payments.Count);
            Assert.AreEqual(800.0, fakePayment.Payments[0], Delta);
        }

        // TC-C07: several orders are each charged their own amount.
        [TestMethod]
        public void ProcessOrder_TwoOrders_ChargesEachAmountInOrder()
        {
            // Arrange
            Order first = new Order("Ali", true, true);
            Order second = new Order("Sara", true, false);

            // Act
            shop.ProcessOrder(first);
            shop.ProcessOrder(second);

            // Assert
            Assert.AreEqual(2, fakePayment.Payments.Count);
            Assert.AreEqual(800.0, fakePayment.Payments[0], Delta);
            Assert.AreEqual(350.0, fakePayment.Payments[1], Delta);
        }

        // TC-C08: the old ProcessOrder(string, bool, bool) gives the same amount.
        [DataTestMethod]
        [DataRow(true, true, 800.0)]
        [DataRow(false, true, 450.0)]
        [DataRow(false, false, 0.0)]
        public void ProcessOrder_OldSignature_ChargesSameAmount(
            bool wantsCoffee, bool wantsSandwich, double expectedAmount)
        {
            // Arrange: shop and fake payment from SetUp

            // Act
            shop.ProcessOrder("Bilal", wantsCoffee, wantsSandwich);

            // Assert
            Assert.AreEqual(expectedAmount, fakePayment.Payments[0], Delta);
        }

        // TC-C09: UpdateMenu prices are used for the next order's payment.
        [TestMethod]
        public void ProcessOrder_AfterUpdateMenu_ChargesNewPrice()
        {
            // Arrange
            shop.UpdateMenu(400, 500);
            Order order = new Order("Ahmed", false, true);

            // Act
            shop.ProcessOrder(order);

            // Assert
            Assert.AreEqual(500.0, fakePayment.Payments[0], Delta);
        }
    }
}
