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
        public void acceptOnlyNickelsDimesAndQuarters()
        {
            Assert.IsTrue(vendFun.isValidCoinType(2));
            Assert.IsTrue(vendFun.isValidCoinType(3));
            Assert.IsTrue(vendFun.isValidCoinType(4));
        }
        [TestMethod]
        public void acceptOnlyCoinsWithValidDimensions()
        {    
            Assert.IsTrue(vendFun.isValidCoinDimensions(5.00,"Weight","grams"));
        }
        
    }
}
