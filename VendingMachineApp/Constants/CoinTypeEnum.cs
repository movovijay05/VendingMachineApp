using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Constants
{
    public class CoinTypeEnum
    {
        //Listing weight,diameter and thickness for Nickels, Dimes and Quarters
        public List<double> acceptable_weight = new List<double> {5.00,2.26,5.67};  //in grams
        public List<double> acceptable_diameter = new List<double> { 0.83, 0.71,0.95 }; // in inches
        public List<double> acceptable_thickness = new List<double> { 1.95, 1.35,1.75 };  // in mm
    }
}