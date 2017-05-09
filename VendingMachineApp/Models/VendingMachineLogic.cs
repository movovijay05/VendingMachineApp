using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Models
{
    public class VendingMachineLogic
    {
        public Boolean isValidCoinType(int coinType)
        {
            Boolean isValidCoin = false;
            switch (coinType)
            {
                case 2:
                    Console.WriteLine("NICKELS");
                    isValidCoin = true;
                    break;
                case 3:
                    Console.WriteLine("DIMES");
                    isValidCoin = true;
                    break;
                case 4:
                    Console.WriteLine("QUATERS");
                    isValidCoin = true;
                    break;
            }
            return isValidCoin;
        }
    }
}