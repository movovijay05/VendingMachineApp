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
            getAllInputValues();
            return View("VendingMachineDisplayView");
        }
        // GET: VendingMachineFormData 
        public ActionResult passVMValues()
        {
            getAllInputValues();

            vCEnum.totalRemainingCashInVM = new Dictionary<string, int> {};
            vCEnum.numberOfNickelsDimesAndQuartersRequiredToMakeChange = new Dictionary<string, int>();
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.QuartersName, Convert.ToInt32(ViewBag.QuartersInVM ?? "0"));
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.DimesName, Convert.ToInt32(ViewBag.DimesInVM ?? "0"));
            vCEnum.totalRemainingCashInVM.Add(CoinTypeEnum.NickelsName, Convert.ToInt32(ViewBag.NickelsInVM ?? "0"));

            ViewBag.DisplayMessage = updateVendingMachineDisplayMessage();
           return View("VendingMachineDisplayView");
        }
        public void getAllInputValues()
        {
            ViewBag.QuartersCount = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtNoOfQuarters"]) ? Request.Form["txtNoOfQuarters"] : "0").ToString();
            ViewBag.NickelsCount = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtNoOfNickels"]) ? Request.Form["txtNoOfNickels"] : "0").ToString();
            ViewBag.DimesCount = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtNoOfDimes"]) ? Request.Form["txtNoOfDimes"] : "0").ToString();
            ViewBag.InvalidCoinsCount = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtNoOfPennies"]) ? Request.Form["txtNoOfPennies"] : "0").ToString();

            ViewBag.Product1Count = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtColaQnty"]) ? Request.Form["txtColaQnty"] : "0").ToString();
            ViewBag.Product2Count = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtChipsQnty"]) ? Request.Form["txtChipsQnty"] : "0").ToString();
            ViewBag.Product3Count = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtCandyQnty"]) ? Request.Form["txtCandyQnty"] : "0").ToString();

            CashInVMBAL cVM = new CashInVMBAL();
            Coin c = cVM.getCashInVM();//could be used for getting data from database

            ViewBag.QuartersInVM = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtQuartersinVM"]) ? Request.Form["txtQuartersinVM"] : c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.QuartersName].ToString()).ToString();
            ViewBag.DimesInVM = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtDimesinVM"]) ? Request.Form["txtDimesinVM"] : c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.DimesName].ToString()).ToString();
            ViewBag.NickelsInVM = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["txtNickelsinVM"]) ? Request.Form["txtNickelsinVM"] : c.CoinNameAndQuantityRemainingInVM[CoinTypeEnum.NickelsName].ToString()).ToString();
        }
        public String displayWelcomeMesssage()
        {
            String displayMessage = "Welcome!!!" + "\n" + "Please select the item you want to purchase \n";
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
            if (ViewBag.InvalidCoinsCount.ToString() != "0")
            {
                resetCounts();
                displayMessage = "Invalid Coins detected.\nThis transaction has been cancelled.\nPlease collect all your coins and retry the transaction with valid coins";
            }
            else
            {
                Dictionary<string, int> dictTotalUserRequestedItems = new Dictionary<string, int>();
                dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product1Name, Convert.ToInt32(ViewBag.Product1Count ?? "0"));
                dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product2Name, Convert.ToInt32(ViewBag.Product2Count ?? "0"));
                dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product3Name, Convert.ToInt32(ViewBag.Product3Count ?? "0"));

                vCEnum.totalPriceOfTransaction = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserRequestedItems, 1);
                displayMessage += "Cash Required: $" + vCEnum.totalPriceOfTransaction + "\n";

                Dictionary<string, int> dictTotalUserDepositedCoins = new Dictionary<string, int>();

                dictTotalUserDepositedCoins.Add(CoinTypeEnum.QuartersName, Convert.ToInt32(ViewBag.QuartersCount ?? "0"));
                dictTotalUserDepositedCoins.Add(CoinTypeEnum.DimesName, Convert.ToInt32(ViewBag.DimesCount ?? "0"));
                dictTotalUserDepositedCoins.Add(CoinTypeEnum.NickelsName, Convert.ToInt32(ViewBag.NickelsCount ?? "0"));

                genFun.updateADictionaryUsingAnotherSimilarDictionary(vCEnum.totalRemainingCashInVM, dictTotalUserDepositedCoins, "ADD");
                vCEnum.totalValueOfCoinsInsertedByTheUser = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserDepositedCoins, 2);

                if ((vCEnum.totalPriceOfTransaction == 0) && (vCEnum.totalValueOfCoinsInsertedByTheUser == 0))
                {
                    VendingMachineDisplayView();
                    displayMessage = displayWelcomeMesssage();
                }
                else if (vCEnum.totalPriceOfTransaction == 0)
                {
                    displayMessage = "Please select product quantity!!CEnum.totalValueOfCoinsInsertedByTheUser!";
                }
                else if (vCEnum.totalValueOfCoinsInsertedByTheUser == 0)
                {
                    displayMessage = "Please input coins!!!";
                }
                else
                {
                    if (vCEnum.totalValueOfCoinsInsertedByTheUser >= vCEnum.totalPriceOfTransaction) { resetCounts(); }
                    displayMessage += "Cash Paid: $" + vCEnum.totalValueOfCoinsInsertedByTheUser + "\n";
                    displayMessage += vendFun.checkIfChangeNeedsToBeProvidedByVMOrUserNeedsToInputMoreCoins(vCEnum);

                    vCEnum.totalRemainingCashInVM = vendFun.updateRemainingCashAfterTendingChangeInVM(vCEnum);

                    ViewBag.QuartersInVM = vCEnum.totalRemainingCashInVM[CoinTypeEnum.QuartersName];
                    ViewBag.DimesInVM = vCEnum.totalRemainingCashInVM[CoinTypeEnum.DimesName];
                    ViewBag.NickelsInVM = vCEnum.totalRemainingCashInVM[CoinTypeEnum.NickelsName];
                }
            }
            return displayMessage;
        }
    }
}
