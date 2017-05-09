using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineApp.Models;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachineLogic vendFun = new VendingMachineLogic();
        GenericFunctions genFun = new GenericFunctions();
        [TestMethod]
        public void checkifVMAcceptsOnlyNickelsDimesAndQuarters()
        {
            Assert.IsTrue(vendFun.isValidCoinType(2));
            Assert.IsTrue(vendFun.isValidCoinType(3));
            Assert.IsTrue(vendFun.isValidCoinType(4));
        }
        [TestMethod]
        public void checkifVMAcceptsOnlyCoinsWithValidDimensions()
        {    
            Assert.IsTrue(vendFun.isValidCoinDimensions(5.00,"Weight","grams"));
            Assert.IsTrue(vendFun.isValidCoinDimensions(1.95,"Thickness", "mm"));
            Assert.IsTrue(vendFun.isValidCoinDimensions(0.71,"Diameter", "inches"));
        }
        [TestMethod]
        public void checkifVMDisplayCorrectValueOfInsertedCoins()
        {
            Assert.AreEqual(0.50, vendFun.calculateMonetaryValueofInsertedCoins("Nickels" ,10));
            Assert.AreEqual(0.20, vendFun.calculateMonetaryValueofInsertedCoins("Dimes", 2));
            Assert.AreEqual(5.00, vendFun.calculateMonetaryValueofInsertedCoins("Quarters", 20));
        }
        [TestMethod]
        public void testVMProductNameAndPricesAreRetreivedCorrectly()
        {
            Dictionary<string, double> checkProductNamesAndPrices = new Dictionary<string, double>();
            checkProductNamesAndPrices.Add("Cola", 1.00);
            checkProductNamesAndPrices.Add("Chips", 0.50);
            checkProductNamesAndPrices.Add("Candy", 0.65);

            Assert.IsTrue(genFun.checkIfTwoDictionariesAreIdenticalWithoutSorting(checkProductNamesAndPrices, vendFun.displayProductDetails()));
        }
    }
}
