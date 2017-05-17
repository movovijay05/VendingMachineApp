using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingMachineApp.Constants;

namespace VendingMachineApp.Models
{
    public class VendingMachineCashEnum
    {
        public static int totalNumberOfQuartersInVM = 10;
        public static int totalNumberOfNickelsiInVM = 10;
        public static int totalNumberOfDimesInVM = 10;

        public const string VMNickelsName = CoinTypeEnum.NickelsName;
        public const string VMDimesName = CoinTypeEnum.DimesName;
        public const string VMQuartersName = CoinTypeEnum.QuartersName;

        public Dictionary<string, int> totalRemainingCashInVM = new Dictionary<string, int> { { CoinTypeEnum.QuartersName, VendingMachineCashEnum.totalNumberOfQuartersInVM
    }, { CoinTypeEnum.DimesName, VendingMachineCashEnum.totalNumberOfDimesInVM
}, { CoinTypeEnum.NickelsName, VendingMachineCashEnum.totalNumberOfNickelsiInVM } };
    }
}