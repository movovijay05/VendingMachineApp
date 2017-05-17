using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Models
{
    public class CashInVMBAL
    {
        public Coin getCashInVM()
        {
            Coin c = new Coin();
            c.CoinNameAndQuantityRemainingInVM.Add(CoinTypeEnum.QuartersName, VendingMachineCashEnum.totalNumberOfQuartersInVM);
            c.CoinNameAndQuantityRemainingInVM.Add(CoinTypeEnum.DimesName, VendingMachineCashEnum.totalNumberOfDimesInVM);
            c.CoinNameAndQuantityRemainingInVM.Add(CoinTypeEnum.NickelsName, VendingMachineCashEnum.totalNumberOfNickelsiInVM);
            return c;
        }
    }
}