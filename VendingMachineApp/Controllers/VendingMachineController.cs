using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachineApp.Models;

namespace VendingMachineApp.Controllers
{
    public class VendingMachineController : Controller
    {
        // GET: VendingMachine
        public ActionResult VendingMachineDisplayView()
        {
            String DisplayMessage = "Welcome!!! Please select the item you want to purchase";
            DisplayMessage += getCoinBalance();
            ViewBag.DisplayMessage = DisplayMessage;
            return View("VendingMachineDisplayView");
        }
        // GET: VendingMachineFormData 
        public ActionResult passVMValues()
        {
            return View("VendingMachineDisplayView");
        }
        public String getCoinBalance()
        {
            String printCoinBalance = "";
            CashInVMBAL cVM = new CashInVMBAL();
            Coin c = cVM.getCashInVM();
            GenericFunctions genFun = new GenericFunctions();
            printCoinBalance = genFun.printAStringIntDictionary(c.CoinNameAndQuantityRemainingInVM);
            return printCoinBalance;
        }
        
    }
}
