using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Models
{
    public class VendingMachineLogic
    {
        VendingMachineProductDetailsEnum vPDetailsEnum = new VendingMachineProductDetailsEnum();
        Dictionary<string, double> productNamesAndPrices = new Dictionary<string, double>();
        Dictionary<string, int> numberOfNickelsDimesAndQuartersRequiredToMakeChange;
        public Boolean isValidCoinType(int coinType)
        {
            Boolean isValidCoin = false;
            switch (coinType)
            {
                case 2:
                    Console.WriteLine(CoinTypeEnum.NickelsName);
                    isValidCoin = true;
                    break;
                case 3:
                    Console.WriteLine(CoinTypeEnum.DimesName);
                    isValidCoin = true;
                    break;
                case 4:
                    Console.WriteLine(CoinTypeEnum.QuartersName);
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
                    if ((metricSystem == CoinTypeEnum.DiameterMetric) && (CoinTypeEnum.acceptable_diameter.Contains(coinDimension)))
                    {
                        isValidCoinDimensions = true;
                    }
                    break;
                case "Weight":
                    if ((metricSystem == CoinTypeEnum.WeightMetric) && (CoinTypeEnum.acceptable_weight.Contains(coinDimension)))
                    {
                        isValidCoinDimensions = true;
                    }
                    break;
                case "Thickness":
                    if ((metricSystem == CoinTypeEnum.ThicknessMetric) && (CoinTypeEnum.acceptable_thickness.Contains(coinDimension)))
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
                case CoinTypeEnum.QuartersName:
                    actualValueofEachCoinType = CoinTypeEnum.QuartersValue;
                    break;
                case CoinTypeEnum.NickelsName:
                    actualValueofEachCoinType = CoinTypeEnum.NickelsValue;
                    break;
                case CoinTypeEnum.DimesName:
                    actualValueofEachCoinType = CoinTypeEnum.DimesValue;
                    break;
            }
            monetaryValueofInsertedCoins = numberOfCoins * actualValueofEachCoinType;
            return monetaryValueofInsertedCoins;
        }

        public Dictionary<string, double> loadProductDetails()
        {
            if (vPDetailsEnum.ProductNames.Count == vPDetailsEnum.ProductPrices.Count)
            {
                for (int i = 0; i < vPDetailsEnum.ProductNames.Count; i++)
                {
                    productNamesAndPrices.Add(vPDetailsEnum.ProductNames[i], vPDetailsEnum.ProductPrices[i]);
                }
                //foreach (var item in productNamesAndPrices)
                //{
                //    Console.WriteLine(item);
                //}
            }
            return productNamesAndPrices;
        }

        public double calculateTotalPriceOfASingleUserTransaction(Dictionary<string, int> itemizedInputList)
        {
            loadProductDetails();
            double totalPriceOfTransaction = 0.00;
            for (int i = 0; i < itemizedInputList.Count; i++)
            {
                double itemPrice = productNamesAndPrices[itemizedInputList.ElementAt(i).Key];
                double itemQuantity = itemizedInputList.ElementAt(i).Value;
                totalPriceOfTransaction += (itemQuantity * itemPrice);
            }
            return totalPriceOfTransaction;
        }

        public double numberOfCoinsRequiredToMakeChange(double changeToBeGivenToTheUser, double coinsValue, String coinsName)
        {
            int numberOfCoinsRequiredToMakeChange = (int)Math.Floor(changeToBeGivenToTheUser / coinsValue);
            numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(coinsName, numberOfCoinsRequiredToMakeChange);
            changeToBeGivenToTheUser = (changeToBeGivenToTheUser - (numberOfCoinsRequiredToMakeChange * coinsValue));
            return changeToBeGivenToTheUser;
        }
        public Dictionary<string, int> calculateTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange(double changeToBeGivenToTheUser)
        {
            numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, int>();
            // The order of the below lines is important
            changeToBeGivenToTheUser = numberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.QuartersValue, CoinTypeEnum.QuartersName);
            changeToBeGivenToTheUser = numberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.DimesValue, CoinTypeEnum.DimesName);
            changeToBeGivenToTheUser = numberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.NickelsValue, CoinTypeEnum.NickelsName);

            //int numberOfQuartersRequiredToMakeChange = (int)Math.Floor(changeToBeGivenToTheUser / CoinTypeEnum.QuartersValue);
            //numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.QuartersName, numberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.QuartersValue));
            //changeToBeGivenToTheUser = (changeToBeGivenToTheUser - (numberOfQuartersRequiredToMakeChange * CoinTypeEnum.QuartersValue));

            //int numberOfDimesRequiredToMakeChange = (int)Math.Floor(changeToBeGivenToTheUser / CoinTypeEnum.DimesValue);
            //numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.DimesName, numberOfDimesRequiredToMakeChange);
            //changeToBeGivenToTheUser = (changeToBeGivenToTheUser - (numberOfDimesRequiredToMakeChange * CoinTypeEnum.DimesValue));

            //int numberofNickelsRequiredToMakeChange = Convert.ToInt32(changeToBeGivenToTheUser / CoinTypeEnum.NickelsValue);
            //numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.NickelsName, numberofNickelsRequiredToMakeChange);
            Trace.WriteLine("-------------------Output Value --------------------");
            foreach (var item in numberOfNickelsDimesAndQuartersRequiredToMakeChange)
            {
                Trace.WriteLine(item);
            }
            return numberOfNickelsDimesAndQuartersRequiredToMakeChange;
        }
    }
}