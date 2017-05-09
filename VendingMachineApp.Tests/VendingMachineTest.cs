using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineApp.Models;

namespace VendingMachineApp.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachineLogic vendFun = new VendingMachineLogic();
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
            //Failure case validation
            Assert.AreEqual(0.00, vendFun.calculateMonetaryValueofInsertedCoins("Quarters", 20));
        }
        
    }
}
