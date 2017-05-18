using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachineApp.Constants;
using VendingMachineApp.Models;

namespace VendingMachineApp.Controllers
{
    public class VendingMachineController : Controller
    {
        VendingMachineLogic vendFun = new VendingMachineLogic();
        GenericFunctions genFun = new GenericFunctions();
        VendingMachineCashEnum vCEnum = new VendingMachineCashEnum();

        // GET: VendingMachine
        public ActionResult VendingMachineDisplayView()
        {
            ViewBag.DisplayMessage = displayWelcomeMesssage();
            resetCounts();
            return View("VendingMachineDisplayView");
        }
        // GET: VendingMachineFormData 
        public ActionResult passVMValues()
        {
            ViewBag.QuartersCount = Convert.ToInt32(Request.Form["txtNoOfQuarters"] ?? "0").ToString();
            ViewBag.NickelsCount = Convert.ToInt32(Request.Form["txtNoOfNickels"] ?? "0").ToString();
            ViewBag.DimesCount = Convert.ToInt32(Request.Form["txtNoOfDimes"] ?? "0").ToString();

            ViewBag.Product1Count = Convert.ToInt32(Request.Form["txtColaQnty"] ?? "0").ToString();
            ViewBag.Product2Count = Convert.ToInt32(Request.Form["txtChipsQnty"] ?? "0").ToString();
            ViewBag.Product3Count = Convert.ToInt32(Request.Form["txtCandyQnty"] ?? "0").ToString();

            ViewBag.QuartersInVM = Convert.ToInt32(Request.Form["txtQuartersinVM"] ?? "10").ToString();
            ViewBag.DimesInVM = Convert.ToInt32(Request.Form["txtDimesinVM"] ?? "10").ToString();
            ViewBag.NickelsInVM = Convert.ToInt32(Request.Form["txtNickelsinVM"] ?? "10").ToString();

            vCEnum.totalRemainingCashInVM = new Dictionary<string, int> {};
            vCEnum.numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, int>();
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.QuartersName, Convert.ToInt32(Request.Form["txtQuartersinVM"] ?? "0"));
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.DimesName, Convert.ToInt32(Request.Form["txtDimesinVM"] ?? "0"));
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.NickelsName, Convert.ToInt32(Request.Form["txtNickelsinVM"] ?? "0"));

            ViewBag.DisplayMessage = updateVendingMachineDisplayMessage();
           return View("VendingMachineDisplayView");
        }

        public String displayWelcomeMesssage()
        {
            String displayMessage = "Welcome!!!" + "\n" + "Please select the item you want to purchase \n";
            getCoinBalance();
            return displayMessage;
        }
        public void resetCounts()
        {
            ViewBag.QuartersCount = "0";
            ViewBag.NickelsCount = "0";
            ViewBag.DimesCount = "0";

            ViewBag.Product1Count = "0";
            ViewBag.Product2Count = "0";
            ViewBag.Product3Count = "0";
        }
        public String updateVendingMachineDisplayMessage()
        {
            String displayMessage = "";

            Dictionary<string, int> dictTotalUserRequestedItems = new Dictionary<string, int>();
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product1Name, Convert.ToInt32(Request.Form["txtColaQnty"] ?? "0"));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product2Name, Convert.ToInt32(Request.Form["txtChipsQnty"] ?? "0"));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product3Name, Convert.ToInt32(Request.Form["txtCandyQnty"] ?? "0"));

            vCEnum.totalPriceOfTransaction = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserRequestedItems,1);
            displayMessage += "Cash Required: $" + vCEnum.totalPriceOfTransaction  + "\n";

            Dictionary<string, int> dictTotalUserDepositedCoins = new Dictionary<string, int>();
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.DimesName, Convert.ToInt32(Request.Form["txtNoOfDimes"] ?? "0"));
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.NickelsName, Convert.ToInt32(Request.Form["txtNoOfNickels"] ?? "0"));
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.QuartersName, Convert.ToInt32(Request.Form["txtNoOfQuarters"] ?? "0"));
            genFun.updateADictionaryUsingAnotherSimilarDictionary(vCEnum.totalRemainingCashInVM, dictTotalUserDepositedCoins, "ADD");
            vCEnum.totalValueOfCoinsInsertedByTheUser = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserDepositedCoins, 2);

            if ((vCEnum.totalPriceOfTransaction  == 0) && (vCEnum.totalValueOfCoinsInsertedByTheUser == 0))
            {
                VendingMachineDisplayView();
                displayMessage = displayWelcomeMesssage();
            }
            else if (vCEnum.totalPriceOfTransaction  == 0)
            {
                displayMessage = "Please select product quantity!!CEnum.totalValueOfCoinsInsertedByTheUser!";
            }
            else if (vCEnum.totalValueOfCoinsInsertedByTheUser == 0)
            {
                displayMessage = "Please input coins!!!";
            }
            else
            {
                if  (vCEnum.totalValueOfCoinsInsertedByTheUser >= vCEnum.totalPriceOfTransaction ) {resetCounts(); }
                displayMessage += "Cash Paid: $" + vCEnum.totalValueOfCoinsInsertedByTheUser + "\n";
                displayMessage += vendFun.checkIfChangeNeedsToBeProvidedByVMOrUserNeedsToInputMoreCoins(vCEnum);

                vCEnum.totalRemainingCashInVM = vendFun.updateRemainingCashAfterTendingChangeInVM(vCEnum);

                ViewBag.QuartersInVM = vCEnum.totalRemainingCashInVM[CoinTypeEnum.QuartersName];
                ViewBag.DimesInVM =  vCEnum.totalRemainingCashInVM[CoinTypeEnum.DimesName];
                ViewBag.NickelsInVM =  vCEnum.totalRemainingCashInVM[CoinTypeEnum.NickelsName];

                if (Convert.ToInt32(Request.Form["txtNoOfPennies"]) > 0)
                {
                    displayMessage += "\nInvalid Coins (Pennies) detected. Returning all Invalid Coins";
                }
            }
            return displayMessage;
        }
        public void getCoinBalance()
        {
            CashInVMBAL cVM = new CashInVMBAL();
            Coin c = cVM.getCashInVM();

            ViewBag.QuartersInVM = Convert.ToInt32(Request.Form["txtQuartersinVM"] ?? c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.QuartersName].ToString()).ToString();
            ViewBag.DimesInVM = Convert.ToInt32(Request.Form["txtDimesinVM"] ?? c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.DimesName].ToString()).ToString();
            ViewBag.NickelsInVM = Convert.ToInt32(Request.Form["txtNickelsinVM"] ?? c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.NickelsName].ToString()).ToString();
        }    
    }
}
