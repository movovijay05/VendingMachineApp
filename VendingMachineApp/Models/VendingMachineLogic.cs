﻿using System;
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
        GenericFunctions genFun = new GenericFunctions();

        Dictionary<string, double> productNamesAndPrices = new Dictionary<string, double>();
        Dictionary<string, double> coinNamesAndValues = new Dictionary<string, double>();
        Dictionary<string, int> numberOfNickelsDimesAndQuartersRequiredToMakeChange;
        Dictionary<string, int> totalRemainingCashInVM = new Dictionary<string, int>() { { CoinTypeEnum.QuartersName, VendingMachineCashEnum.totalNumberOfQuartersInVM } , { CoinTypeEnum.DimesName, VendingMachineCashEnum.totalNumberOfDimesInVM } , { CoinTypeEnum.NickelsName, VendingMachineCashEnum.totalNumberOfNickelsiInVM } };
        
        public VendingMachineLogic()
        {
            loadCoinDetails();
            loadProductDetails();
        }
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
            }
            return productNamesAndPrices;
        }

        public Dictionary<string, double> loadCoinDetails()
        {
            if (CoinTypeEnum.CoinNames.Count == CoinTypeEnum.CoinValues.Count)
            {
                for (int i = 0; i < CoinTypeEnum.CoinNames.Count; i++)
                {
                    coinNamesAndValues.Add(CoinTypeEnum.CoinNames[i], CoinTypeEnum.CoinValues[i]);
                }
            }
            return productNamesAndPrices;
        }

        public double calculateTotalPriceOfASingleUserTransaction(Dictionary<string, int> itemizedInputList, Int32 type)
        {
            double totalPriceOfTransaction = 0.00;
            for (int i = 0; i < itemizedInputList.Count; i++)
            {
                double itemPrice = 0.00;
                if (type == 1)
                {
                    itemPrice = productNamesAndPrices[itemizedInputList.ElementAt(i).Key];
                }else
                {
                    itemPrice = coinNamesAndValues[itemizedInputList.ElementAt(i).Key];
                }
                double itemQuantity = itemizedInputList.ElementAt(i).Value;
                totalPriceOfTransaction += (itemQuantity * itemPrice);
            }
            return totalPriceOfTransaction;
        }

        public int calculateNumberOfCoinsRequiredToMakeChange(double changeToBeGivenToTheUser, double coinsValue, String coinsName)
        {
            int numberOfCoinsRequiredToMakeChange = 0;
            if (coinsName.Equals(CoinTypeEnum.NickelsName)) {
                numberOfCoinsRequiredToMakeChange = Convert.ToInt32(changeToBeGivenToTheUser / coinsValue);
            } else {
                numberOfCoinsRequiredToMakeChange = (int)Math.Floor(changeToBeGivenToTheUser / coinsValue);
            }
            return numberOfCoinsRequiredToMakeChange;
        }
        public double remainingChangeToBeGivenToTheUser(double changeToBeGivenToTheUser, double coinsValue, String coinsName) {
            int numberOfCoinsRequiredToMakeChange = calculateNumberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser,  coinsValue,  coinsName);
            numberOfNickelsDimesAndQuartersRequiredToMakeChange.Add(coinsName, numberOfCoinsRequiredToMakeChange);
            changeToBeGivenToTheUser = (changeToBeGivenToTheUser - (numberOfCoinsRequiredToMakeChange * coinsValue));
            return changeToBeGivenToTheUser;
        }
        public Dictionary<string, int> calculateTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange(double changeToBeGivenToTheUser)
        {
            numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, int>();
            // The order of the below lines is important
            changeToBeGivenToTheUser = calculateNumberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.QuartersValue, CoinTypeEnum.QuartersName);
            changeToBeGivenToTheUser = calculateNumberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.DimesValue, CoinTypeEnum.DimesName);
            changeToBeGivenToTheUser = calculateNumberOfCoinsRequiredToMakeChange(changeToBeGivenToTheUser, CoinTypeEnum.NickelsValue, CoinTypeEnum.NickelsName);
            return numberOfNickelsDimesAndQuartersRequiredToMakeChange;
        }

        public string checkIfChangeNeedsToBeProvidedByVMOrUserNeedsToInputMoreCoins(double totalValueOfCoinsInsertedByTheUser, double totalPriceOfTransaction)
        {
            String messageToBeDisplayed = "";
            double balanceToBeDisplayed = genFun.calculateBalance(totalPriceOfTransaction, totalValueOfCoinsInsertedByTheUser);
            if (totalPriceOfTransaction > totalValueOfCoinsInsertedByTheUser){
                messageToBeDisplayed = "Please pay the remanining balance of $" + balanceToBeDisplayed.ToString();
            }else if (totalPriceOfTransaction < totalValueOfCoinsInsertedByTheUser)
            {
                double changeToBeGivenToTheUser = totalValueOfCoinsInsertedByTheUser - totalPriceOfTransaction;
                if (checkIfThereisEnoughCashInVMAndUpdateRemainingCash(balanceToBeDisplayed) == true) {
                    updateRemainingCashAfterTendingChangeInVM();
                    
                    messageToBeDisplayed = "Thanks for paying!!! Please collect the remanining balance of $" + balanceToBeDisplayed + "\n Remaining Cash in VM:" + genFun.printAStringIntDictionary(totalRemainingCashInVM);
                }
            }
            //Trace.WriteLine(balanceToBeDisplayed.ToString());
            return balanceToBeDisplayed.ToString();
        }
        public bool checkIfThereisEnoughCashInVMAndUpdateRemainingCash(double changeToBeGivenToTheUser)
        {
            bool isThereEnoughCashInVMToTendChange = true;
            numberOfNickelsDimesAndQuartersRequiredToMakeChange = calculateTheNumberOfNickelsDimesAndQuartersRequiredToMakeChange(changeToBeGivenToTheUser);
            if (numberOfNickelsDimesAndQuartersRequiredToMakeChange.Count.Equals(totalRemainingCashInVM))
            {
                foreach (int i in Enumerable.Range(1, numberOfNickelsDimesAndQuartersRequiredToMakeChange.Count))
                {
                    if (totalRemainingCashInVM.ElementAt(i).Value < numberOfNickelsDimesAndQuartersRequiredToMakeChange.ElementAt(i).Value)
                    {
                        isThereEnoughCashInVMToTendChange = false;
                    }
                }
                
            }
            return isThereEnoughCashInVMToTendChange;
        }
        //call the updateRemainingCashAfterTendingChangeInVM method only if checkIfThereisEnoughCashInVMAndUpdateRemainingCash returns true
        public void updateRemainingCashAfterTendingChangeInVM()
        {
            foreach (int i in Enumerable.Range(1, numberOfNickelsDimesAndQuartersRequiredToMakeChange.Count))
            {
                totalRemainingCashInVM[totalRemainingCashInVM.ElementAt(i).Key] = totalRemainingCashInVM.ElementAt(i).Value - numberOfNickelsDimesAndQuartersRequiredToMakeChange.ElementAt(i).Value;
            }
        }
    }
}