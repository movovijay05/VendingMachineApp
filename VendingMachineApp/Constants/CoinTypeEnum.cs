using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Constants
{
    public class CoinTypeEnum
    {
        //Nickels dimensions
        static double NickelsWeight = 5.00;
        static double NickelsDiameter = 0.83;
        static double NickelsThickness = 1.95;

        //Dimes dimensions
        static double DimesWeight = 2.26;
        static double DimesDiameter = 0.71;
        static double DimesThickness = 1.35;

        // Quarters dimensions
        static double QuartersWeight = 5.67;
        static double QuartersDiameter = 0.95;
        static double QuartersThickness = 1.75;

        //Dimension Metrics
        public string WeightMetric = "grams";
        public string DiameterMetric = "inches";
        public string ThicknessMetric = "mm";

        //Monetary Value
        public double NickelsValue = 0.05;
        public double DimesValue = 0.10;
        public double QuartersValue = 0.25;

        //Listing weight,diameter and thickness for Nickels, Dimes and Quarters
        public List<double> acceptable_weight = new List<double> { NickelsWeight, DimesWeight, QuartersWeight };  //in grams
        public List<double> acceptable_diameter = new List<double> { NickelsDiameter, DimesDiameter, QuartersDiameter }; // in inches
        public List<double> acceptable_thickness = new List<double> { NickelsThickness, DimesThickness, QuartersThickness };  // in mm
    }
}