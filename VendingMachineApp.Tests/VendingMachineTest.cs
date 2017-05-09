using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineApp.Models;

namespace VendingMachineApp.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void acceptOnlyNickelsDimesAndQuarters()
        {
            VendingMachineLogic vendFun = new VendingMachineLogic();
            Assert.IsTrue(vendFun.isValidCoinType(2));
            Assert.IsTrue(vendFun.isValidCoinType(3));
            Assert.IsTrue(vendFun.isValidCoinType(4));
        }
    }
}
