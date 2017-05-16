using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Models
{
    public class Coin
    {
        public Coin() {
            CoinNameAndQuantityRemainingInVM = new Dictionary<String, int>();
         }
        public Dictionary<String, int> CoinNameAndQuantityRemainingInVM { get; set; }
    }
}