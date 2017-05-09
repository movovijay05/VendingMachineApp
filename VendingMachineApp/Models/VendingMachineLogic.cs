using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Models
{
    public class VendingMachineLogic
    {
        VendingMachineProductDetailsEnum vPDetailsEnum = new VendingMachineProductDetailsEnum();
        Dictionary<string, double> productNamesAndPrices = new Dictionary<string, double>();
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

        public Dictionary<string, double> calculateTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange(double changeToBeGivenToTheUser)
        {
            Dictionary<string, double> numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, double>();
            
            if (changeToBeGivenToTheUser > CoinTypeEnum.QuartersValue)
            {
                int numberofQuartersRequiredToMakeChange = Convert.ToInt32(changeToBeGivenToTheUser / CoinTypeEnum.QuartersValue);
                numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.QuartersName, numberofQuartersRequiredToMakeChange);
                changeToBeGivenToTheUser = (changeToBeGivenToTheUser % CoinTypeEnum.QuartersValue);
            }
            else if(changeToBeGivenToTheUser > CoinTypeEnum.DimesValue){
                int numberofDimesRequiredToMakeChange = Convert.ToInt32(changeToBeGivenToTheUser / CoinTypeEnum.QuartersValue);
                numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.DimesName, numberofDimesRequiredToMakeChange);
                changeToBeGivenToTheUser = (changeToBeGivenToTheUser % CoinTypeEnum.QuartersValue);
            }
            else
            {
                int numberofNickelssRequiredToMakeChange = Convert.ToInt32(changeToBeGivenToTheUser / CoinTypeEnum.QuartersValue);
                numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(CoinTypeEnum.NickelsName, numberofNickelssRequiredToMakeChange);
            }
            foreach (var item in numberOfNickelsDimesAndQuartersRequiredToMakeChange)
            {
                Console.WriteLine(item);
            }
            return numberOfNickelsDimesAndQuartersRequiredToMakeChange;
        }
    }
}