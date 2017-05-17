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
            String DisplayMessage = "Welcome!!!" + "\n" + "Please select the item you want to purchase";
            DisplayMessage += "\n" + getCoinBalance();
            ViewBag.DisplayMessage = DisplayMessage;
            return View("VendingMachineDisplayView");
        }
        // GET: VendingMachineFormData 
        public ActionResult passVMValues()
        {
           ViewBag.DisplayMessage = updateVendingMachineDisplayMessage();
           return View("VendingMachineDisplayView");
        }

        public String updateVendingMachineDisplayMessage()
        {
            String displayMessage = "";

            Dictionary<string, int> dictTotalUserRequestedItems = new Dictionary<string, int>();
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product1Name, Convert.ToInt32(Request.Form["txtColaQnty"] ?? "0"));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product2Name, Convert.ToInt32(Request.Form["txtChipsQnty"] ?? "0"));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product3Name, Convert.ToInt32(Request.Form["txtCandyQnty"] ?? "0"));

            double totalPriceOfASingleUserTransaction = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserRequestedItems,1);
            displayMessage += "Cash Required: $" + totalPriceOfASingleUserTransaction + "\n";

            Dictionary<string, int> dictTotalUserDepositedCoins = new Dictionary<string, int>();
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.DimesName, Convert.ToInt32(Request.Form["txtNoOfDimes"] ?? "0"));
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.NickelsName, Convert.ToInt32(Request.Form["txtNoOfNickels"] ?? "0"));
            dictTotalUserDepositedCoins.Add(CoinTypeEnum.QuartersName, Convert.ToInt32(Request.Form["txtNoOfQuarters"] ?? "0"));
            genFun.updateADictionaryUsingAnotherSimilarDictionary(vCEnum.totalRemainingCashInVM, dictTotalUserDepositedCoins, "ADD");
            double totalCashDepositedByUserForCurrentTransaction = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserDepositedCoins, 2);
            displayMessage += "Cash Paid: $" + totalCashDepositedByUserForCurrentTransaction + "\n";
            displayMessage += vendFun.checkIfChangeNeedsToBeProvidedByVMOrUserNeedsToInputMoreCoins(totalCashDepositedByUserForCurrentTransaction, totalPriceOfASingleUserTransaction);

            if (Convert.ToInt32(Request.Form["txtNoOfPennies"]) > 0)
            {
                displayMessage += "\nInvalid Coins (Pennies) detected. Returning all Invalid Coins";
            }
            return displayMessage;
        }
        public String getCoinBalance()
        {
            String printCoinBalance = "";
            CashInVMBAL cVM = new CashInVMBAL();
            Coin c = cVM.getCashInVM();
            printCoinBalance = "Cash Remaining" + genFun.printAStringIntDictionary(c.CoinNameAndQuantityRemainingInVM);
            return printCoinBalance;
        }    
    }
}
