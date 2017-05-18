using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineApp.Models;
using System.Collections.Generic;
using System.Diagnostics;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachineLogic vendFun = new VendingMachineLogic();
        GenericFunctions genFun = new GenericFunctions();
        VendingMachineCashEnum vCEnum = new VendingMachineCashEnum();
        [TestMethod]
        public void testifVMAcceptsOnlyNickelsDimesAndQuarters()
        {
            Assert.IsTrue(vendFun.isValidCoinType(2));
            Assert.IsTrue(vendFun.isValidCoinType(3));
            Assert.IsTrue(vendFun.isValidCoinType(4));
        }
        [TestMethod]
        public void testifVMAcceptsOnlyCoinsWithValidDimensions()
        {    
            Assert.IsTrue(vendFun.isValidCoinDimensions(5.00,"Weight","grams"));
            Assert.IsTrue(vendFun.isValidCoinDimensions(1.95,"Thickness", "mm"));
            Assert.IsTrue(vendFun.isValidCoinDimensions(0.71,"Diameter", "inches"));
        }
        [TestMethod]
        public void testifVMDisplayCorrectValueOfInsertedCoins()
        {
            Assert.AreEqual(0.50, vendFun.calculateMonetaryValueofInsertedCoins("NICKELS" ,10));
            Assert.AreEqual(0.20, vendFun.calculateMonetaryValueofInsertedCoins("DIMES", 2));
            Assert.AreEqual(5.00, vendFun.calculateMonetaryValueofInsertedCoins("QUARTERS", 20));
        }
        [TestMethod]
        public void testVMProductNameAndPricesAreRetreivedCorrectly()
        {
            Dictionary<string, double> checkProductNamesAndPrices = new Dictionary<string, double>();
            checkProductNamesAndPrices.Add("Cola", 1.00);
            checkProductNamesAndPrices.Add("Chips", 0.50);
            checkProductNamesAndPrices.Add("Candy", 0.65);

            Assert.IsTrue(genFun.checkIfTwoStringDoubleDictionariesAreIdenticalWithoutSorting(checkProductNamesAndPrices, vendFun.loadProductDetails()));
        }
        [TestMethod]
        public void testTotalPriceOfASingleUserTransaction()
        {
            Dictionary<string, int> checkTotalPriceOfUserRequestedItems = new Dictionary<string, int>();
            checkTotalPriceOfUserRequestedItems.Add("Cola", 3);   // 1 * 3 = 3
            checkTotalPriceOfUserRequestedItems.Add("Chips", 1); // 0.50 * 1 = 0.50
            checkTotalPriceOfUserRequestedItems.Add("Candy", 5); // 0.65 * 5 = 3.25
            Assert.AreEqual(6.75, vendFun.calculateTotalPriceOfASingleUserTransaction(checkTotalPriceOfUserRequestedItems,1));
        }
        [TestMethod]
        public void testTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange()
        {
            vCEnum.numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, int>();
            vCEnum.totalRemainingCashInVM = new Dictionary<string, int>();
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.QuartersName, 20);
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.DimesName, 10);
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.NickelsName, 10);

            Dictionary<string, int> checkTheNumberOfNickelsDimesAndQuartersRequired = new Dictionary<string, int>();
            // order of the below items is important
            checkTheNumberOfNickelsDimesAndQuartersRequired.Add("QUARTERS", 10);
            checkTheNumberOfNickelsDimesAndQuartersRequired.Add("DIMES", 1);
            checkTheNumberOfNickelsDimesAndQuartersRequired.Add("NICKELS", 1);
            vCEnum.changeToBeGivenToTheUser = 2.65;
            
            vCEnum.numberOfNickelsDimesAndQuartersRequiredToMakeChange = vendFun.calculateTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange(vCEnum);
            Assert.IsTrue(genFun.checkIfTwoStringIntDictionariesAreIdenticalWithoutSorting(checkTheNumberOfNickelsDimesAndQuartersRequired, vCEnum.numberOfNickelsDimesAndQuartersRequiredToMakeChange));
        }
        [TestMethod]
        public void testIfChangeNeedsToBeProvidedByVMOrUserNeedsToInputMoreCoins()
        {
            Assert.AreEqual("1.25", genFun.calculateBalance(7.50, 8.75).ToString());
        }
    }
}
