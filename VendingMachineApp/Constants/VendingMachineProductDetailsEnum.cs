using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Constants
{
    public class VendingMachineProductDetailsEnum
    {
        //string Product1Name = "Cola";
        //double Product1Price = 1.00;

        //string Product2Name = "Chips";
        //double Product2Price = 0.50;

        //string Product3Name = "Candy";
        //double Product3Price = 0.65;

        public List<string> ProductNames = new List<string> { "Cola", "Chips", "Candy" };
        public List<double> ProductPrices = new List<double> { 1.00, 0.50, 0.65 };
    }
}