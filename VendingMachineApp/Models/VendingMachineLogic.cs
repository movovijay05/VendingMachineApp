using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Models
{
    public class VendingMachineLogic
    {
        CoinTypeEnum cTypEnum = new CoinTypeEnum();
        public Boolean isValidCoinType(int coinType)
        {
            Boolean isValidCoin = false;
            switch (coinType)
            {
                case 2:
                    Console.WriteLine("NICKELS");
                    isValidCoin = true;
                    break;
                case 3:
                    Console.WriteLine("DIMES");
                    isValidCoin = true;
                    break;
                case 4:
                    Console.WriteLine("QUATERS");
                    isValidCoin = true;
                    break;
            }
            return isValidCoin;
        }
        public Boolean isValidCoinDimensions(double coinDimension, String dimensionName, String metricSystem)
        {
            Boolean isValidCoinDimensions = false;
            switch (dimensionName)
            {
                case "Diameter":
                    if ((metricSystem == cTypEnum.DiameterMetric) && (cTypEnum.acceptable_diameter.Contains(coinDimension)))
                    {
                        isValidCoinDimensions = true;
                    }
                    break;
                case "Weight":
                    if ((metricSystem == cTypEnum.WeightMetric) && (cTypEnum.acceptable_weight.Contains(coinDimension)))
                    {
                        isValidCoinDimensions = true;
                    }
                    break;
                case "Thickness":
                    if ((metricSystem == cTypEnum.ThicknessMetric) && (cTypEnum.acceptable_thickness.Contains(coinDimension)))
                    {
                        isValidCoinDimensions = true;
                    }
                    break;
            }           
            return isValidCoinDimensions;
        }
    }
}