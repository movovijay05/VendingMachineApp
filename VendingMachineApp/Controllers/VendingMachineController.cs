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
            String DisplayMessage = updateVendingMachineDisplayMessage();
            return View("VendingMachineDisplayView");
        }

        public String updateVendingMachineDisplayMessage()
        {
            Dictionary<string, int> dictTotalUserRequestedItems = new Dictionary<string, int>();
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product1Name, Convert.ToInt32(Request.Form["txtColaQnty"]));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product2Name, Convert.ToInt32(Request.Form["txtChipsQnty"]));
            dictTotalUserRequestedItems.Add(VendingMachineProductDetailsEnum.Product3Name, Convert.ToInt32(Request.Form["txtCandyQnty"]));
            double totalPriceOfASingleUserTransaction = vendFun.calculateTotalPriceOfASingleUserTransaction(dictTotalUserRequestedItems,1);

            return "";
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
