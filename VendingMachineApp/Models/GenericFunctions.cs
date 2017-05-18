using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Models
{
    public class GenericFunctions
    {
        public double calculateBalance(double number1, double number2)
        {
            double balance = Math.Round(number2 - number1, 2);
            return balance;
        }
        public bool checkIfTwoStringDoubleDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, double> oldDict,
      Dictionary<string, double> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count))
            {
                return false;
            }
            else
            {
                // iterate through all the keys in oldDict and
                // verify whether the key exists in the newDict
                foreach (int i in Enumerable.Range(0, oldDict.Count - 1))
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
        }
        public bool checkIfTwoStringIntDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, int> oldDict,
      Dictionary<string, int> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count)) return false;

            // iterate through all the keys in oldDict and
            // verify whether the key exists in the newDict
            foreach (int i in Enumerable.Range(0, oldDict.Count - 1))
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

        public String printAStringIntDictionary(Dictionary<string, int> printableStringIntDictionary)
        {
            String dictionaryStringOutput = "";
            foreach (var item in printableStringIntDictionary)
            {
                dictionaryStringOutput += "\n" + item;
            }
            dictionaryStringOutput = dictionaryStringOutput.Replace("[", "")
                .Replace("]", "")
                .Replace(",", ":");
            return dictionaryStringOutput;
        }

        public Dictionary<string, int> updateADictionaryUsingAnotherSimilarDictionary(Dictionary<string, int> updateableDict,
      Dictionary<string, int> updaterDict, String Operation)
        {
            foreach (int j in Enumerable.Range(0, updateableDict.Count))
            {
                if (updaterDict.ContainsKey(updateableDict.ElementAt(j).Key))
                {
                    if (Operation == "ADD")
                    {
                        updateableDict[updateableDict.ElementAt(j).Key] = updateableDict.ElementAt(j).Value + updaterDict[updateableDict.ElementAt(j).Key];
                    }
                    else
                    {
                        updateableDict[updateableDict.ElementAt(j).Key] = updateableDict.ElementAt(j).Value - updaterDict[updateableDict.ElementAt(j).Key];
                    }
                }
            }
            return updateableDict;
        }
    }
}