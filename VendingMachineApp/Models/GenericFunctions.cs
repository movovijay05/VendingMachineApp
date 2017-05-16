﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Models
{
    public class GenericFunctions
    {
        public double calculateBalance(double number1, double number2)
        {
            double balance = number2 - number1;
            return balance;
        }
        public bool checkIfTwoStringDoubleDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, double> oldDict,
      Dictionary<string, double> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count)) return false;

            // iterate through all the keys in oldDict and
            // verify whether the key exists in the newDict
            foreach (int i in Enumerable.Range(1, oldDict.Count))
            {
                if (newDict.ElementAt(i).Equals(oldDict.ElementAt(i)))
                {
                    // iterate through each value for the current key in oldDict and 
                    // verify whether or not it exists for the current key in the newDict
                }
                else { return false; }
            }
            return true;
        }
        public bool checkIfTwoStringIntDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, int> oldDict,
      Dictionary<string, int> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count)) return false;

            // iterate through all the keys in oldDict and
            // verify whether the key exists in the newDict
            foreach (int i in Enumerable.Range(1, oldDict.Count))
            {
                if (newDict.ElementAt(i).Equals(oldDict.ElementAt(i)))
                {
                    // iterate through each value for the current key in oldDict and 
                    // verify whether or not it exists for the current key in the newDict
                }
                else { return false; }
            }
            return true;
        }

        public String printAStringIntDictionary(Dictionary<string, int> printableStringIntDcitionary)
        {
            String dictionaryStringOutput = "";
            foreach (var item in printableStringIntDcitionary)
            {
                dictionaryStringOutput += item;
            }
            return dictionaryStringOutput;
        }
    }
}