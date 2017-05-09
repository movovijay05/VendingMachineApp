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
        VendingMachineProductDetailsEnum vPDetailsEnum = new VendingMachineProductDetailsEnum();
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

        public Boolean isValidCoinDimensions(double coinDimension, string dimensionName, string metricSystem)
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

        public double calculateMonetaryValueofInsertedCoins(string coinType, int numberOfCoins)
        {
            double monetaryValueofInsertedCoins = 0.00;
            double actualValueofEachCoinType = 0.00;
            switch (coinType)
            {
                case "Quarters":
                    actualValueofEachCoinType = cTypEnum.QuartersValue;
                    break;
                case "Nickels":
                    actualValueofEachCoinType = cTypEnum.NickelsValue;
                    break;
                case "Dimes":
                    actualValueofEachCoinType = cTypEnum.DimesValue;
                    break;
            }
            monetaryValueofInsertedCoins = numberOfCoins * actualValueofEachCoinType;
            return monetaryValueofInsertedCoins;
        }

        public Dictionary<string,double> displayProductDetails()
        {

            Dictionary<string, double> productNamesAndPrices = new Dictionary<string, double> ();
            if (vPDetailsEnum.ProductNames.Count == vPDetailsEnum.ProductPrices.Count)
            {
                //var productNamesAndPrices = vPDetailsEnum.ProductNames.Zip(vPDetailsEnum.ProductPrices, (first, second) => first + " " + second);
                for (int i = 0; i < vPDetailsEnum.ProductNames.Count; i++)
                {
                    productNamesAndPrices.Add(vPDetailsEnum.ProductNames[i], vPDetailsEnum.ProductPrices[i]);

                }
                    foreach (var item in productNamesAndPrices)
                {
                    Console.WriteLine(item);
                }
            }
            return productNamesAndPrices;
        }
    }
}